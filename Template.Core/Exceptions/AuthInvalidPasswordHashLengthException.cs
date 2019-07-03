using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthInvalidPasswordHashLengthException : ArgumentException
    {
        public AuthInvalidPasswordHashLengthException() : base(AuthenticationStrings.PasswordHashNotCorrect) { }

        public AuthInvalidPasswordHashLengthException(string param) : base(AuthenticationStrings.PasswordHashNotCorrect, param) { }
    }
}
