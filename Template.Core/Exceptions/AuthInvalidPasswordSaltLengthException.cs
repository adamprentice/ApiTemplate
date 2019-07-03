using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthInvalidPasswordSaltLengthException : ArgumentException
    {
        public AuthInvalidPasswordSaltLengthException() : base(AuthenticationStrings.PasswordSaltNotCorrect) { }

        public AuthInvalidPasswordSaltLengthException(string param) : base(AuthenticationStrings.PasswordSaltNotCorrect, param) { }
    }
}
