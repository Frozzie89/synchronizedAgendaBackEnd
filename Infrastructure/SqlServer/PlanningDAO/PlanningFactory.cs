using System.Data.SqlClient;
using TI_BackEnd.Domain.Planning;

namespace TI_BackEnd.Infrastructure.SqlServer.PlanningDAO
{
    public class PlanningFactory : IFactory<Planning>
    {
        public Planning CreateFromReader(SqlDataReader reader)
        {
            return new Planning
            {
                Id = reader.GetInt32(reader.GetOrdinal(PlanningQueries.ColId)),
                LabelPlanning = reader.GetString(reader.GetOrdinal(PlanningQueries.ColLabel)),
                IdSuperUser = reader.GetInt32(reader.GetOrdinal(PlanningQueries.ColIdSuperUser)),
            };
        }
    }
}