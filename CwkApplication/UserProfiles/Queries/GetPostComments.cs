using CwkApplication.Models;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.Queries
{
    public class GetPostComments : IRequest<OperationResault<List<PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
