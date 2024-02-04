using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.UserProfiles.Queries;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.QueryHandler
{
    public class GetPostCommentsHandler : IRequestHandler<GetPostComments, OperationResault<List<PostComment>>>
    {
        private readonly DataContext _ctx;
        public GetPostCommentsHandler( DataContext ctx)
        {                
            _ctx = ctx;
        }
        public async Task<OperationResault<List<PostComment>>> Handle(GetPostComments request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<List<PostComment>>();
            try
            {
                var post = await _ctx.Posts
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.PostId == request.PostId);
                result.PayLoad =post.Comments.ToList();

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
