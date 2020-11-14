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

        private IUserFactory _userFactory = new UserFactory();

        public IEnumerable<User> Query()
        {
            IList<User> users = new List<User>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = UserQueries.ReqQuery;
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

                command.CommandText = UserQueries.ReqCreate;

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

                command.CommandText = UserQueries.ReqDelete;
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

                command.CommandText = UserQueries.ReqDelete;
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

                command.CommandText = UserQueries.ReqGetById;
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

                command.CommandText = UserQueries.ReqGetByEmail;
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

                    command.CommandText = UserQueries.ReqUpdateById;
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

                    command.CommandText = UserQueries.ReqUpdateByEmail;
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

        public User GetAuthentication(string email, string password)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = UserQueries.ReqAuthentication;
                command.Parameters.AddWithValue($"@{ColEmail}", email);
                command.Parameters.AddWithValue($"@{ColPassword}", password);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _userFactory.CreateFromReader(reader);

                return null;
            }
        }
    }
}