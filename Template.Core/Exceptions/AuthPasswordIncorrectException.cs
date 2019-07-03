using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthPasswordMissingException : ArgumentException
    {
        public AuthPasswordMissingException() : base(AuthenticationStrings.PasswordMissing) { }

        public AuthPasswordMissingException(string param) : base(AuthenticationStrings.PasswordMissing, param) { }
    }
}
