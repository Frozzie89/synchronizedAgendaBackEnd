using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using TI_BackEnd.Domain.User;
using System.IO;

namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public class UserRepository : IUserRepository
    {
        private IFactory<User> _userFactory = new UserFactory();

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

                var userCryptedPassword = encryptSha256(user.Password);

                command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", user.Email);
                command.Parameters.AddWithValue($"@{UserQueries.ColLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{UserQueries.ColFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{UserQueries.ColUserName}", user.UserName);
                command.Parameters.AddWithValue($"@{UserQueries.ColPassword}", userCryptedPassword);

                user.Password = userCryptedPassword;
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
                command.Parameters.AddWithValue($"@{UserQueries.ColId}", id);

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
                command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", email);

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
                command.Parameters.AddWithValue($"@{UserQueries.ColId}", id);


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
                command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", email);


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
                    command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", user.Email);
                    command.Parameters.AddWithValue($"@{UserQueries.ColLastName}", user.LastName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColFirstName}", user.FirstName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColUserName}", user.UserName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColPassword}", user.Password);
                    command.Parameters.AddWithValue($"@{UserQueries.ColId}", id);

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
                    command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", user.Email);
                    command.Parameters.AddWithValue($"@{UserQueries.ColLastName}", user.LastName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColFirstName}", user.FirstName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColUserName}", user.UserName);
                    command.Parameters.AddWithValue($"@{UserQueries.ColPassword}", user.Password);
                    command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", email);

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

                var userCryptedPassword = encryptSha256(password);

                command.CommandText = UserQueries.ReqAuthentication;
                command.Parameters.AddWithValue($"@{UserQueries.ColEmail}", email);
                command.Parameters.AddWithValue($"@{UserQueries.ColPassword}", userCryptedPassword);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _userFactory.CreateFromReader(reader);

                return null;
            }
        }

        private string encryptSha256(string value)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(value);

            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(passwordBytes);

            string hashString = "";

            foreach (var b in hash)
            {
                hashString += b.ToString("x2");
            }

            hashString = hashString.Substring(0, 50);

            return hashString;
        }
    }
}