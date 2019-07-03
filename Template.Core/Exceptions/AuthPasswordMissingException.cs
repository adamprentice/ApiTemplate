using System;
using System.Collections.Generic;
using System.Text;
using Template.Core.StaticStrings;

namespace Template.Core.Exceptions
{
    public class AuthPasswordIncorrectException : Exception
    {
        public AuthPasswordIncorrectException() : base(AuthenticationStrings.PasswordIncorrect) { }
    }
}
