using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Queries;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.QueryHandler
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, OperationResault<PostModel>>
    {
        private readonly DataContext _ctx;
        public GetPostByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResault<PostModel>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<PostModel>();

            var post = await _ctx.Posts.FirstOrDefaultAsync(x => x.PostId == request.PostId);
            if (post is null)
            {
                var error = new Error() { code = ErrorCode.ServerError, Massage = "this post is empty"};
                result.IsError = true;
                result.Errors.Add(error);
            }
            result.PayLoad = post;
            return result;


        }
    }
}
