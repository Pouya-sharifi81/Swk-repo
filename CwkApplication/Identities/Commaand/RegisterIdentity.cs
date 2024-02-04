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
    public class RegisterIdentity : IRequest<OperationResault<IdentityUserProfileDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}
