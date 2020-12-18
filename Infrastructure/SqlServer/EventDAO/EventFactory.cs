using System.Data.SqlClient;
using TI_BackEnd.Domain.Event;

namespace TI_BackEnd.Infrastructure.SqlServer.EventDAO
{
    public class EventFactory : IFactory<Event>
    {
        public Event CreateFromReader(SqlDataReader reader)
        {
            return new Event
            {
                IdEventCategory = reader.GetInt32(reader.GetOrdinal(EventQueries.ColIdEventCategory)),
                IdPlanning = reader.GetInt32(reader.GetOrdinal(EventQueries.ColIdPlanning)),
                Label = reader.GetString(reader.GetOrdinal(EventQueries.ColLabel)),
                Start = reader.GetDateTime(reader.GetOrdinal(EventQueries.ColStart)),
                End = reader.GetDateTime(reader.GetOrdinal(EventQueries.ColEnd))
            };
        }
    }
}