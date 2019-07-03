using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template.Core.Exceptions;
using Template.Domain;
using Template.Domain.Entities;

namespace Template.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            this._context = context;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new AuthEmailMissingException(nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new AuthPasswordMissingException(nameof(password));
            }

            var user = _context.Users.SingleOrDefault(x => x.EmailAddress == email);

            if (user == null)
            {
                throw new AuthEmailNotFoundException(email);
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AuthPasswordMissingException(nameof(password));
            }


            if (_context.Users.Any(x => x.EmailAddress == user.EmailAddress))
            {
                throw new AuthEmailAlreadyExistsException(user.EmailAddress);
            }


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
            {
                throw new AuthEmailNotFoundException(userParam.EmailAddress);
            }

            if (userParam.EmailAddress != user.EmailAddress)
            {
                if (_context.Users.Any(x => x.EmailAddress == userParam.EmailAddress))
                {
                    throw new AuthEmailAlreadyExistsException(userParam.EmailAddress);
                }
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.EmailAddress = userParam.EmailAddress;

            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null)
            {
                throw new AuthUserNotFoundException(id);
            }

            _context.Users.Remove(user);
            _context.SaveChanges();            
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
            {
                throw new AuthPasswordMissingException(nameof(password));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
            {
                throw new AuthPasswordMissingException(nameof(password));
            }
            if (storedHash.Length != 64)
            {
                throw new AuthInvalidPasswordHashLengthException(nameof(storedHash));
            }
            if (storedSalt.Length != 128)
            {
                throw new AuthInvalidPasswordSaltLengthException(nameof(storedSalt));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
