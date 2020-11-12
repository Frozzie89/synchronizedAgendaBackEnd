using System.Collections.Generic;
namespace TI_BackEnd.Domain.User
{

    public interface IUserRepository
    {
        IEnumerable<User> Query();
        User Get(int id);
        User Get(string email);
        User GetAuthentication(string email, string password);
        User Create(User user);
        bool Delete(int id);
        bool Delete(string email);
        bool Update(int id, User user);
        bool Update(string email, User user);
    }
}