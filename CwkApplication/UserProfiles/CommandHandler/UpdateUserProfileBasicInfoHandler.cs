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
    public class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo, OperationResault<UserProfile>>
    {
        public readonly DataContext _ctx;
        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResault<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<UserProfile>();
            
            try
            {

                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(x => x.UserProfileId == request.UserProfileId);
                if (userProfile is null)
                {
                    result.IsError = true;
                    var error = new Error() { code = ErrorCode.NotFound , Massage = $"no user profile with id{request.UserProfileId}" };
                    result.Errors.Add(error);
                    return result;
                }
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress,
                    request.Phone, request.DateOfBirth, request.CurrentCity);
                userProfile.UpdateBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync();
                result.PayLoad = userProfile;
                return result;
            }
            catch (Exception e)
            {
                var error = new Error() { code = ErrorCode.ServerError, Massage = e.Message };
                result.IsError = true;
                result.Errors.Add(error);
            }



            return result;

        }
    }
}
