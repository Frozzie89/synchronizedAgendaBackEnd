using System.Collections.Generic;
namespace TI_BackEnd.Domain.User
{

    public interface IUserRepository
    {
        IEnumerable<User> Query();
        User Get(int id, string userName);
        User Create(User user);
        bool Delete(int id, string userName);
        bool Update(int id, string userName, User user);
    }
}