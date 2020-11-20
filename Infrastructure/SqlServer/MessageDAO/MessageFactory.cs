using System.Data.SqlClient;
using TI_BackEnd.Domain.Message;

namespace TI_BackEnd.Infrastructure.SqlServer.MessageDAO
{
    public class MessageFactory : IFactory<Message>
    {
        public Message CreateFromReader(SqlDataReader reader)
        {
            return new Message
            {
                Id = reader.GetInt32(reader.GetOrdinal(MessageQueries.ColId)),
                IdChat = reader.GetInt32(reader.GetOrdinal(MessageQueries.ColIdChat)),
                IdUser = reader.GetInt32(reader.GetOrdinal(MessageQueries.ColIdUser)),
                Body = reader.GetString(reader.GetOrdinal(MessageQueries.ColBody))
            };
        }
    }
}