using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Event;
using TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;

namespace TI_BackEnd.Infrastructure.SqlServer.EventDAO
{
    public class EventRepository : IEventRepository
    {
        private EventFactory _eventFactory = new EventFactory();
        private EventCategoryRepository _eventCategoryRepository = new EventCategoryRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();

        public Event Create(Event eventt)
        {
            // interdiction de créer un évenement avec une catégorie qui n'existe pas
            if (_eventCategoryRepository.Get(eventt.IdEventCategory) == null)
                return null;

            // interdiction de crééer un évenement dans un planning qui n'existe pas
            if (_planningRepository.Get(eventt.IdPlanning) == null)
                return null;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventQueries.ReqCreate;
                command.Parameters.AddWithValue($"@{EventQueries.ColIdEventCategory}", eventt.IdEventCategory);
                command.Parameters.AddWithValue($"@{EventQueries.ColIdPlanning}", eventt.IdPlanning);
                command.Parameters.AddWithValue($"@{EventQueries.ColLabel}", eventt.Label);
                command.Parameters.AddWithValue($"@{EventQueries.ColStart}", eventt.Start);
                command.Parameters.AddWithValue($"@{EventQueries.ColEnd}", eventt.End);

                eventt.Id = (int)command.ExecuteScalar();
            }

            return eventt;
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventQueries.ReqDelete;
                command.Parameters.AddWithValue($"@{EventQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteAllFromPlanning(int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventQueries.ReqDeleteFromPlanning;
                command.Parameters.AddWithValue($"@{EventQueries.ColIdPlanning}", idPlanning);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public Event Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventQueries.ReqGet;
                command.Parameters.AddWithValue($"@{EventQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _eventFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Event> Query()
        {
            IList<Event> events = new List<Event>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = EventQueries.ReqQuery;

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    events.Add(_eventFactory.CreateFromReader(reader));
            }

            return events;
        }

        public IEnumerable<Event> QueryFromPlanning(int idPlanning)
        {
            IList<Event> events = new List<Event>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = EventQueries.ReqQueryFromPlanning;
                command.Parameters.AddWithValue($"@{EventQueries.ColIdPlanning}", idPlanning);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    events.Add(_eventFactory.CreateFromReader(reader));
            }

            return events;
        }

        public bool Update(int id, Event eventt)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = EventQueries.ReqUpdate;
                command.Parameters.AddWithValue($"@{EventQueries.ColLabel}", eventt.Label);
                command.Parameters.AddWithValue($"@{EventQueries.ColStart}", eventt.Start);
                command.Parameters.AddWithValue($"@{EventQueries.ColEnd}", eventt.End);
                command.Parameters.AddWithValue($"@{EventQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}