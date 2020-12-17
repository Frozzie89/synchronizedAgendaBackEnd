using System.Data.SqlClient;
using TI_BackEnd.Domain.EventCategory;

namespace TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO
{
    public class EventCategoryFactory : IFactory<EventCategory>
    {
        public EventCategory CreateFromReader(SqlDataReader reader)
        {
            return new EventCategory
            {
                Id = reader.GetInt32(reader.GetOrdinal(EventCategoryQueries.ColId)),
                Label = reader.GetString(reader.GetOrdinal(EventCategoryQueries.ColLabel)),
                Color = reader.GetString(reader.GetOrdinal(EventCategoryQueries.ColColor))
            };
        }
    }
}