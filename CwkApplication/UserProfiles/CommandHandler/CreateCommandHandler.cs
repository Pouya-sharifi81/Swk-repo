using AutoMapper;
using CwkApplication.Models;
using CwkApplication.UserProfiles.Commands;
using CwkDataAcces;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.CommandHandler
{
    public class CreateCommandHandler : IRequestHandler<CreateUserCommand, OperationResault<UserProfile>>
    {
        public readonly DataContext _ctx;
        public CreateCommandHandler(DataContext ctx )
        {
            _ctx = ctx;
        }
        public async Task<OperationResault<UserProfile>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<UserProfile>();
            var bacisInfo = BasicInfo.CreateBasicInfo(
                request.FirstName, request.LastName, request.EmailAddress,
                request.Phone, request.DateOfBirth, request.CurrentCity
                );
            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), bacisInfo);   
            _ctx.UserProfiles.Add(userProfile);
            await _ctx.SaveChangesAsync();

            result.PayLoad = userProfile;

            return result;
        }
    }
}
