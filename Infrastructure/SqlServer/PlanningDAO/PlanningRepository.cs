using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Planning;

namespace TI_BackEnd.Infrastructure.SqlServer.PlanningDAO
{
    public class PlanningRepository : IPlanningRepository
    {
        public static readonly string TableName = "Planning";
        public static readonly string ColId = "idPlanning";
        public static readonly string ColLabel = "labelPlanning";
        public static readonly string ColIdSuperUser = "superUser";


        private IFactory<Planning> _planningFactory = new PlanningFactory();

        public Planning Create(Planning planning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqCreate;

                command.Parameters.AddWithValue($"@{ColLabel}", planning.LabelPlanning);
                command.Parameters.AddWithValue($"@{ColIdSuperUser}", planning.IdSuperUser);

                planning.Id = (int)command.ExecuteScalar();
            }

            return planning;
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqDelete;
                command.Parameters.AddWithValue($"@{ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public Planning Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = PlanningQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{ColId}", id);


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
                command.Parameters.AddWithValue($"@{ColLabel}", LabelPlanning);


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
                command.Parameters.AddWithValue($"@{ColIdSuperUser}", IdSuperUser);


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

        public bool Update(int id, Planning planning)
        {
            if (Get(id) == null)
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = PlanningQueries.ReqQuery;
                    command.Parameters.AddWithValue($"@{ColLabel}", planning.LabelPlanning);
                    command.Parameters.AddWithValue($"@{ColId}", id);

                    return command.ExecuteNonQuery() == 1;
                }

            }
            return false;
        }
    }
}