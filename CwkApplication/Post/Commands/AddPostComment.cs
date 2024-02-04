using CwkApplication.Models;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.Commands
{
    public class AddPostComment : IRequest<OperationResault<PostComment>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public string CommentText { get; set; }
    }
}
