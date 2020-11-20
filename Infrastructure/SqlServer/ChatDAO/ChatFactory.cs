using System.Data.SqlClient;
using TI_BackEnd.Domain.Chat;

namespace TI_BackEnd.Infrastructure.SqlServer.ChatDAO
{
    public class ChatFactory : IFactory<Chat>
    {
        public Chat CreateFromReader(SqlDataReader reader)
        {
            return new Chat
            {
                Id = reader.GetInt32(reader.GetOrdinal(ChatQueries.ColId)),
                IdPlanning = reader.GetInt32(reader.GetOrdinal(ChatQueries.ColIdPlanning)),
            };
        }
    }
}