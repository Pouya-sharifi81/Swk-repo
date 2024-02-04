using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.UserProfiles.Queries;
using CwkDataAcces;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.QueryHandler
{
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById,OperationResault< UserProfile>>
    {
        public readonly DataContext _ctr;
        public GetUserProfileByIdHandler(DataContext ctr)
        {
            _ctr = ctr;
        }
        public async Task<OperationResault<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            var result =new OperationResault<UserProfile>();
           var profile =await _ctr.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            if (profile is null)
            {
                result.IsError = true;
                var error = new Error() { code = ErrorCode.NotFound, Massage = $"no user profile with id{request.UserProfileId}" };
                result.Errors.Add(error);
                return result;
            }
            result.PayLoad = profile;
            return result;
        }
    }
}
