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
    public class DeletePost : IRequest<OperationResault<PostModel>>
    {
        public Guid userProfileId { get; set; }
        public Guid postId { get; set; }
    }
}
