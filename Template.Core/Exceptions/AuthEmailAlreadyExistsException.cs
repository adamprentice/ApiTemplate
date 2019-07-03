using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthEmailAlreadyExistsException : Exception
    {
        public AuthEmailAlreadyExistsException(string emailAddress) 
            : base(string.Format(AuthenticationStrings.EmailAlreadyExists, emailAddress)) { }
    }
}
