using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthEmailMissingException : ArgumentException
    {
        public AuthEmailMissingException() : base(AuthenticationStrings.EmailMissing) { }

        public AuthEmailMissingException(string param) : base(AuthenticationStrings.EmailMissing, param) { }
    }
}
