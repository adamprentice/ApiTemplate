using System.Collections.Generic;
using Template.Domain.Entities;

namespace Template.Services.UserService
{
    public interface IUserService
    {
        User Authenticate(string email, string password);

        User Create(User user, string password);

        void Delete(int id);

        IEnumerable<User> GetAll();

        User GetById(int id);

        void Update(User userParam, string password = null);
    }
}