using CwkApplication.Identities.Dtos;
using CwkApplication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Identities.Query
{
    public class GetCurrentUser : IRequest<OperationResault<IdentityUserProfileDto>>
    {
        public Guid UserProfileId { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
