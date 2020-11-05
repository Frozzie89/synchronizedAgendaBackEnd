using System.Collections.Generic;
namespace TI_BackEnd.Domain.User
{

    public interface IUserRepository
    {
        IEnumerable<User> Query();
        User Get(int id);
        User Create(User user);
        bool Delete(int id);
        bool Update(int id, User user);
    }
}