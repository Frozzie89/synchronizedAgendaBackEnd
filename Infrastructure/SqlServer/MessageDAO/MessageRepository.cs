using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain;
using TI_BackEnd.Domain.Chat;
using TI_BackEnd.Domain.Message;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Infrastructure.SqlServer.MessageDAO
{
    public class MessageRepository : IMessageRepository
    {
        private IFactory<Message> _messageFactory = new MessageFactory();
        private IRepository<User> _userRepository = new UserRepository();
        private IRepository<Chat> _chatRepository = new ChatRepository();


        public Message Create(Message message)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                if (_userRepository.Get(message.IdUser) == null || _chatRepository.Get(message.IdChat) == null)
                    return null;

                command.CommandText = MessageQueries.ReqCreate;
                command.Parameters.AddWithValue($"@{MessageQueries.ColIdChat}", message.IdChat);
                command.Parameters.AddWithValue($"@{MessageQueries.ColIdUser}", message.IdUser);
                command.Parameters.AddWithValue($"@{MessageQueries.ColBody}", message.Body);

                message.Id = (int)command.ExecuteScalar();
            }

            return message;
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MessageQueries.ReqDelete;
                command.Parameters.AddWithValue($"@{MessageQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteAllFromChat(int idChat)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MessageQueries.ReqDeleteAllFromChat;
                command.Parameters.AddWithValue($"@{MessageQueries.ColIdChat}", idChat);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public Message Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MessageQueries.ReqGet;
                command.Parameters.AddWithValue($"@{MessageQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _messageFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Message> Query()
        {
            IList<Message> messages = new List<Message>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MessageQueries.ReqQuery;

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    messages.Add(_messageFactory.CreateFromReader(reader));
            }

            return messages;
        }

        public IEnumerable<Message> QueryFromChat(int idChat)
        {
            IList<Message> messages = new List<Message>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MessageQueries.ReqQueryFromChat;
                command.Parameters.AddWithValue($"@{MessageQueries.ColIdChat}", idChat);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    messages.Add(_messageFactory.CreateFromReader(reader));
            }

            return messages;
        }

        public bool Update(int id, Message message)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MessageQueries.ReqQuery;
                command.Parameters.AddWithValue($"@{MessageQueries.ColBody}", message.Body);
                command.Parameters.AddWithValue($"@{MessageQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}