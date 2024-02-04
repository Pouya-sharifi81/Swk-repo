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
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Identities.CommandHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResault<IdentityUserProfileDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;
        private OperationResault<IdentityUserProfileDto> _result = new();

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager,
            IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResault<IdentityUserProfileDto>> Handle(LoginCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken:
                        cancellationToken);


                _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.PayLoad.UserName = identityUser.UserName;
                _result.PayLoad.Token = GetJwtString(identityUser, userProfile);
                return _result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Username);

            if (identityUser is null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist,
                    IdentityErrorMessages.NonExistentIdentityUser);

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);

            return identityUser;
        }

        private string GetJwtString(IdentityUser identityUser, UserProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("IdentityId", identityUser.Id),
            new Claim("UserProfileId", userProfile.UserProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}