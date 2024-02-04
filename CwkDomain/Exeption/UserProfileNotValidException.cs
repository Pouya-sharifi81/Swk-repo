using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkDomain.Exeption
{
    public class UserProfileNotValidException : DomainModelInvalidException
    {
        internal UserProfileNotValidException() { }
        internal UserProfileNotValidException(string message) : base(message) { }
        internal UserProfileNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
