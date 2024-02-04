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
    public class GetAllUserProfileHandler : IRequestHandler<GetAllUserProfile,OperationResault< IEnumerable<UserProfile>>>
    {
        public readonly DataContext _ctx;
        public GetAllUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResault< IEnumerable<UserProfile>>> Handle(GetAllUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<IEnumerable<UserProfile>>();
            result.PayLoad = await _ctx.UserProfiles.ToListAsync();
            return result;
        } 
    }
}
