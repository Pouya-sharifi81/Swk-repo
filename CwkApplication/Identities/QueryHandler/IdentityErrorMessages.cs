﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Identities.QueryHandler
{

    public class IdentityErrorMessages
    {
        public const string NonExistentIdentityUser = "Unable to find a user with the specified username";
        public const string IncorrectPassword = "The provided password is incorrect";
        public const string IdentityUserAlreadyExists = "Provided email address already exists. Cannot register new user";
        public const string UnauthorizedAccountRemoval = "Cannot remove account as you are not its owner";
    }
}
