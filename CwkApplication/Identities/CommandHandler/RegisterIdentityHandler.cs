using AutoMapper;
using AutoMapper.Configuration;
using CwkApplication.Enums;
using CwkApplication.Identities.Commaand;
using CwkApplication.Identities.Dtos;
using CwkApplication.Identities.QueryHandler;
using CwkApplication.Models;
using CwkApplication.Option;
using CwkApplication.services;
using CwkDataAcces;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using CwkDomain.Exeption;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Identities.CommandHandler
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResault<IdentityUserProfileDto>>
    {

        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResault<IdentityUserProfileDto> _result = new();
        private readonly IMapper _mapper;

        public RegisterIdentityHandler(DataContext ctx, UserManager<IdentityUser> userManager,
            IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResault<IdentityUserProfileDto>> Handle(RegisterIdentity request,
            CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdentityDoesNotExist(request);
                if (_result.IsError) return _result;

                await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(request, transaction, cancellationToken);
                if (_result.IsError) return _result;

                var profile = await CreateUserProfileAsync(request, transaction, identity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(profile);
                _result.PayLoad.UserName = identity.UserName;
                _result.PayLoad.Token = GetJwtString(identity, profile);
                return _result;
            }

            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => _result.AddError(ErrorCode.ValidationError, e));
            }

            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task ValidateIdentityDoesNotExist(RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Username);

            if (existingIdentity != null)
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);

        }

        private async Task<IdentityUser> CreateIdentityUserAsync(RegisterIdentity request,
            IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.Username, UserName = request.Username };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var identityError in createdIdentity.Errors)
                {
                    _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }
            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(RegisterIdentity request,
            IDbContextTransaction transaction, IdentityUser identity,
            CancellationToken cancellationToken)
        {
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.Username,
                    request.Phone, request.DateOfBirth, request.CurrentCity);

                var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
                _ctx.UserProfiles.Add(profile);
                await _ctx.SaveChangesAsync(cancellationToken);
                return profile;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim("IdentityId", identity.Id),
            new Claim("UserProfileId", profile.UserProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}