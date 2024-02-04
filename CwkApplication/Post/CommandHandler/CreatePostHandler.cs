using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Commands;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkDomain.Exeption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.CommandHandler
{
    public class CreatePostHandler : IRequestHandler<CreatePost, OperationResault<PostModel>>
    {
        private readonly DataContext _ctx;
        public CreatePostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResault<PostModel>> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var reault = new OperationResault<PostModel>();
            try
            {
                var newPost = PostModel.CreatePost(request.UserProfileId, request.TextContent);
                await _ctx.Posts.AddAsync(newPost);
                await _ctx.SaveChangesAsync();
                reault.PayLoad = newPost;
            }
          

            catch (Exception e)
            {
               reault.Errors.Add(new Error { code = ErrorCode.UnknownError, Massage="is post not know" });
               reault.IsError = true;
            }

            return reault;
        }
    }
}
