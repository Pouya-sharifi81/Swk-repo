using CwkApplication.Models;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.UserProfiles.Commands
{
    public class DeleteUserProfile : IRequest<OperationResault<UserProfile>>

    {
        public Guid UserProfileId { get; set; }
    }
}
