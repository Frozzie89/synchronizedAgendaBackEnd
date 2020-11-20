using System.Data.SqlClient;
using TI_BackEnd.Domain.User;

namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public class UserFactory : IFactory<User>
    {
        public User CreateFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal(UserQueries.ColId)),
                Email = reader.GetString(reader.GetOrdinal(UserQueries.ColEmail)),
                LastName = reader.GetString(reader.GetOrdinal(UserQueries.ColLastName)),
                FirstName = reader.GetString(reader.GetOrdinal(UserQueries.ColFirstName)),
                UserName = reader.GetString(reader.GetOrdinal(UserQueries.ColUserName)),
                Password = reader.GetString(reader.GetOrdinal(UserQueries.ColPassword))
            };
        }
    }
}