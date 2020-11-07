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
            WHERE {ColId} = @{ColId} AND {ColEmail} = @{ColEmail}";

        public static readonly string ReqUpdateById = $@"
            UPDATE [{TableName}]
            SET 
            {ColEmail} = @{ColEmail},
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColUserName} = @{ColUserName},
            {ColPassword} = @{ColPassword}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdateByEmail = $@"
            UPDATE [{TableName}]
            SET 
            {ColEmail} = @{ColEmail},
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColUserName} = @{ColUserName},
            {ColPassword} = @{ColPassword}
            WHERE {ColEmail} = @{ColEmail}";

        public static readonly string ReqGetById = ReqQuery + $@" WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByEmail = ReqQuery + $@" WHERE {ColEmail} = @{ColEmail}";

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

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqDelete;
                command.Parameters.AddWithValue($"@{ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }
        public bool Delete(string email)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqDelete;
                command.Parameters.AddWithValue($"@{ColEmail}", email);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public User Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqGetById;
                command.Parameters.AddWithValue($"@{ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _userFactory.CreateFromReader(reader);

                return null;
            }
        }

        public User Get(string email)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ReqGetByEmail;
                command.Parameters.AddWithValue($"@{ColEmail}", email);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _userFactory.CreateFromReader(reader);

                return null;
            }
        }

        public bool Update(int id, User user)
        {
            if (Get(id) == null)
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = ReqUpdateById;
                    command.Parameters.AddWithValue($"@{ColEmail}", user.Email);
                    command.Parameters.AddWithValue($"@{ColLastName}", user.LastName);
                    command.Parameters.AddWithValue($"@{ColFirstName}", user.FirstName);
                    command.Parameters.AddWithValue($"@{ColUserName}", user.UserName);
                    command.Parameters.AddWithValue($"@{ColPassword}", user.Password);
                    command.Parameters.AddWithValue($"@{ColId}", id);

                    return command.ExecuteNonQuery() == 1;
                }

            }
            return false;

        }

        public bool Update(string email, User user)
        {
            if (Get(email) == null)
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = ReqUpdateByEmail;
                    command.Parameters.AddWithValue($"@{ColEmail}", user.Email);
                    command.Parameters.AddWithValue($"@{ColLastName}", user.LastName);
                    command.Parameters.AddWithValue($"@{ColFirstName}", user.FirstName);
                    command.Parameters.AddWithValue($"@{ColUserName}", user.UserName);
                    command.Parameters.AddWithValue($"@{ColPassword}", user.Password);
                    command.Parameters.AddWithValue($"@{ColEmail}", email);

                    return command.ExecuteNonQuery() == 1;
                }
            }
            return false;
        }
    }
}