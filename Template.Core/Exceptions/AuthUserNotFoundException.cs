using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthUserNotFoundException : Exception
    {
        public AuthUserNotFoundException(int userId) 
            : base(string.Format(AuthenticationStrings.UserNotFound, userId)) { }
    }
}
