using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Core.StaticStrings
{
    public static class AuthenticationStrings
    {
        public static string PasswordMissing => "Password is required";

        public static string PasswordIncorrect => "Password is incorrect";

        public static string EmailMissing => "Email is required";

        public static string EmailAlreadyExists => "Email address {0} already exists";

        public static string EmailNotFound => "User with email address {0} not found";

        public static string UserNotFound => "User with id {0} not found";

        public static string PasswordHashNotCorrect => "Invalid length of password hash (64 bytes expected)";

        public static string PasswordSaltNotCorrect => "Invalid length of password salt (128 bytes expected";
    }
}
