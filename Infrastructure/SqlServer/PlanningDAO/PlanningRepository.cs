using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Domain.Chat;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;
using TI_BackEnd.Infrastructure.SqlServer.InvitationDAO;
using TI_BackEnd.Services;

namespace TI_BackEnd.Infrastructure.SqlServer.PlanningDAO
{
    public class PlanningRepository : IPlanningRepository
    {
        private IFactory<Planning> _planningFactory = new PlanningFactory();
        private ChatRepository _chatRepository = new ChatRepository();
        private PlanningService _planningService = new PlanningService();

        public Planning Create(Planning planning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                if (!_planningService.canCreate(planning))
                    return null;

                command.CommandText = PlanningQueries.ReqCreate;
                command.Parameters.AddWithValue($"@{PlanningQueries.ColLabel}", planning.LabelPlanning);
                command.Parameters.AddWithValue($"@{PlanningQueries.ColIdSuperUser}", planning.IdSuperUser);

                planning.Id = (int)command.ExecuteScalar();

                // un chat est automatiquement créé lors de la création du planning
                Chat chat = new Chat { IdPlanning = planning.Id };
                _chatRepository.Create(chat);

            }

            return planning;
        }

        public Planning Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{PlanningQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _planningFactory.CreateFromReader(reader);

                return null;
            }
        }

        public Planning GetByLabel(string LabelPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqGetByLabel;
                command.Parameters.AddWithValue($"@{PlanningQueries.ColLabel}", LabelPlanning);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _planningFactory.CreateFromReader(reader);

                return null;
            }
        }

        public Planning GetBySuperUser(int IdSuperUser)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqGetBySuperUser;
                command.Parameters.AddWithValue($"@{PlanningQueries.ColIdSuperUser}", IdSuperUser);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _planningFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Planning> Query()
        {
            IList<Planning> plannings = new List<Planning>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = PlanningQueries.ReqQuery;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    plannings.Add(_planningFactory.CreateFromReader(reader));
            }

            return plannings;
        }
    }
}