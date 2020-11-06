using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.User;

namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public class UserRepository : IUserRepository
    {
        public static readonly string TableName = "User";
        public static readonly string ColId = "idUser";
        public static readonly string ColEmail = "email";
        public static readonly string ColLastName = "lastname";
        public static readonly string ColFirstName = "firstname";
        public static readonly string ColUserName = "username";
        public static readonly string ColPassword = "password";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";
        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColEmail}, {ColLastName}, {ColFirstName}, {ColUserName}, {ColPassword})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColEmail}, @{ColLastName}, @{ColFirstName}, @{ColUserName}, @{ColPassword})";


        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId} AND {ColUserName} = @{ColUserName}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET 
            {ColEmail} = @{ColEmail},
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColUserName} = @{ColUserName},
            {ColPassword} = @{ColPassword}
            WHERE {ColId} = @{ColId} AND {ColUserName} = @{ColUserName}";

        public static readonly string ReqGet = ReqQuery + $@" WHERE {ColId} = @{ColId} AND {ColUserName} = @{ColUserName}";

        private IUserFactory _userFactory = new UserFactory();

        public IEnumerable<User> Query()
        {
            IList<User> users = new List<User>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = ReqQuery;

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    users.Add(_userFactory.CreateFromReader(reader));
            }

            return users;
        }

        public User Create(User user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqCreate;

                command.Parameters.AddWithValue($"@{ColEmail}", user.Email);
                command.Parameters.AddWithValue($"@{ColLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{ColFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{ColUserName}", user.UserName);
                command.Parameters.AddWithValue($"@{ColPassword}", user.Password);

                user.Id = (int)command.ExecuteScalar();
            }

            return user;
        }

        public bool Delete(int id, string userName)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqDelete;
                command.Parameters.AddWithValue($"@{ColId}", id);
                command.Parameters.AddWithValue($"@{ColUserName}", userName);

                // renvoie le nombre de résultats
                return command.ExecuteNonQuery() == 1;
            }
        }

        public User Get(int id, string userName)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqGet;
                command.Parameters.AddWithValue($"@{ColId}", id);
                command.Parameters.AddWithValue($"@{ColUserName}", userName);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _userFactory.CreateFromReader(reader);

                return null;
            }
        }

        public bool Update(int id, string userName, User user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqUpdate;
                command.Parameters.AddWithValue($"@{ColEmail}", user.Email);
                command.Parameters.AddWithValue($"@{ColLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{ColFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{ColUserName}", user.UserName);
                command.Parameters.AddWithValue($"@{ColPassword}", user.Password);
                command.Parameters.AddWithValue($"@{ColId}", id);
                command.Parameters.AddWithValue($"@{ColUserName}", userName);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}