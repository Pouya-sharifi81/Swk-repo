using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.UserProfiles.Commands;
using CwkDataAcces;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.CommandHandler
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile , OperationResault<UserProfile>>
    {

        private readonly DataContext _ctx;
        public DeleteUserProfileHandler(DataContext ctx) { _ctx = ctx; }
        public async Task<OperationResault<UserProfile>> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<UserProfile>();

            var userProfile =await _ctx.UserProfiles.FirstOrDefaultAsync(x=>x.UserProfileId == request.UserProfileId );
             _ctx.UserProfiles.Remove(userProfile);
           await _ctx.SaveChangesAsync();

            if (userProfile is null)
            {
                result.IsError = true;
                var error = new Error() { code = ErrorCode.NotFound, Massage = $"no user profile with id{request.UserProfileId}" };
                result.Errors.Add(error);
                return result;
            }
            result.PayLoad = userProfile;

            return result;
        }
    }
}
