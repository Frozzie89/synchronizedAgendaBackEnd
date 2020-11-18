using System.Collections.Generic;
namespace TI_BackEnd.Domain.User
{

    public interface IUserRepository : IRepository<User>
    {
        User Get(string email);
        User GetAuthentication(string email, string password);
        bool Delete(string email);
        bool Update(string email, User user);
    }
}