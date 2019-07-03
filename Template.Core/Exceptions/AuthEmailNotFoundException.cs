using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthEmailNotFoundException : Exception
    {
        public AuthEmailNotFoundException(string emailAddress) 
            : base(string.Format(AuthenticationStrings.EmailNotFound, emailAddress)) { }
    }
}
