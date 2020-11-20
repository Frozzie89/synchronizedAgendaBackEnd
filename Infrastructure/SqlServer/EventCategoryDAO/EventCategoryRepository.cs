using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain;
using TI_BackEnd.Domain.EventCategory;

namespace TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO
{
    public class EventCategoryRepository : IEventCategoryRepository
    {
        private IFactory<EventCategory> _eventCategoryFactory = new EventCategoryFactory();

        public EventCategory Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventCategoryQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{EventCategoryQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _eventCategoryFactory.CreateFromReader(reader);

                return null;
            }
        }

        public EventCategory GetByLabel(string label)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventCategoryQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{EventCategoryQueries.ColLabel}", label);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _eventCategoryFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<EventCategory> Query()
        {
            IList<EventCategory> eventCategories = new List<EventCategory>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = EventCategoryQueries.ReqQuery;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    eventCategories.Add(_eventCategoryFactory.CreateFromReader(reader));
            }

            return eventCategories;
        }
    }
}