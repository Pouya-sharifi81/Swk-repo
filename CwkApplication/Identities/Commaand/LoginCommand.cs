using CwkApplication.Identities.Dtos;
using CwkApplication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Identities.Commaand
{
    public class LoginCommand : IRequest<OperationResault<IdentityUserProfileDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
