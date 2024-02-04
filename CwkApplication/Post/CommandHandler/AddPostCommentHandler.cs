using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Commands;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkDomain.Exeption;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.CommandHandler
{

    public class AddPostCommentHandler : IRequestHandler<AddPostComment, OperationResault<PostComment>>
    {
        private readonly DataContext _ctx;
        public AddPostCommentHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResault<PostComment>> Handle(AddPostComment request, CancellationToken cancellationToken)
        {
            var result = new OperationResault<PostComment>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId,
                    cancellationToken: cancellationToken);
               

                var comment = PostComment.CreatePostComment(request.PostId, request.CommentText, request.UserProfileId);

                post.AddPostComment(comment);

                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = comment;

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
