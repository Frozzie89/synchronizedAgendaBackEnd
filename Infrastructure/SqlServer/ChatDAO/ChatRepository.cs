using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Chat;

namespace TI_BackEnd.Infrastructure.SqlServer.ChatDAO
{
    public class ChatRepository : IChatRepository
    {
        private IFactory<Chat> _chatFactory = new ChatFactory();

        public Chat Create(Chat chat)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqCreate;

                command.Parameters.AddWithValue($"@{ChatQueries.ColIdPlanning}", chat.IdPlanning);

                chat.Id = (int)command.ExecuteScalar();
            }

            return chat;
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqDeleteById;
                command.Parameters.AddWithValue($"@{ChatQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteByPlanningId(int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqDeleteByPlanningId;
                command.Parameters.AddWithValue($"@{ChatQueries.ColIdPlanning}", idPlanning);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public Chat Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{ChatQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _chatFactory.CreateFromReader(reader);

                return null;
            }
        }

        public Chat GetByPlanningId(int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqGetByIdPlanning;
                command.Parameters.AddWithValue($"@{ChatQueries.ColIdPlanning}", idPlanning);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _chatFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Chat> Query()
        {
            IList<Chat> chats = new List<Chat>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = ChatQueries.ReqQuery;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    chats.Add(_chatFactory.CreateFromReader(reader));
            }

            return chats;
        }

        public bool Update(int id, Chat chat)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = ChatQueries.ReqQuery;
                command.Parameters.AddWithValue($"@{ChatQueries.ColIdPlanning}", chat.IdPlanning);
                command.Parameters.AddWithValue($"@{ChatQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}