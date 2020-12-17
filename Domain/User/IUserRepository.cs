using System.Collections.Generic;
namespace TI_BackEnd.Domain.User
{

    public interface IUserRepository : IRepositoryCreate<User>
    {
        User Get(string email);
        User GetAuthentication(string email, string password);
    }
}