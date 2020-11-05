using System.Data.SqlClient;
using TI_BackEnd.Domain.User;

namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public class UserFactory : IUserFactory
    {
        public User CreateFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal(UserRepository.ColId)),
                Email = reader.GetString(reader.GetOrdinal(UserRepository.ColEmail)),
                LastName = reader.GetString(reader.GetOrdinal(UserRepository.ColLastName)),
                FirstName = reader.GetString(reader.GetOrdinal(UserRepository.ColFirstName)),
                UserName = reader.GetString(reader.GetOrdinal(UserRepository.ColUserName)),
                Password = reader.GetString(reader.GetOrdinal(UserRepository.ColPassword))
            };
        }
    }
}