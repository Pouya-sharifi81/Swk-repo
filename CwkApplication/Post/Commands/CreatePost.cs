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
    public class CreatePost : IRequest<OperationResault<PostModel>>
    {
        public Guid UserProfileId { get; set; }
        public string TextContent { get; set; }
    }
}
