using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Commands;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.CommandHandler
{
    public class DeletePostHandler : IRequestHandler<DeletePost, OperationResault<PostModel>>
    {
        private readonly DataContext _ctx;
        public DeletePostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResault<PostModel>> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<PostModel>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(x => x.PostId == request.postId);
                if (post is null)
                {
                    var error = new Error() { code = ErrorCode.ServerError, Massage = "this post is empty" };
                    result.IsError = true;
                    result.Errors.Add(error);
                    return result;
                }
                _ctx.Posts.Remove(post);

                await _ctx.SaveChangesAsync();

                result.PayLoad = post;
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
