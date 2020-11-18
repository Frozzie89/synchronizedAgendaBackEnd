using System.Collections.Generic;
using TI_BackEnd.Domain.Planning;

namespace TI_BackEnd.Infrastructure.SqlServer.PlanningDAO
{
    public class PlanningRepository : IPlanningRepository
    {
        public static readonly string TableName = "Planning";
        public static readonly string ColId = "idPlanning";
        public static readonly string ColLabel = "labelPlanning";
        public static readonly string ColIdSuperUser = "superUser";


        public Planning Create(Planning className)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Planning Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Planning GetByLabel(string LabelPlanning)
        {
            throw new System.NotImplementedException();
        }

        public Planning GetBySuperUser(int IdSuperUser)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Planning> Query()
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, Planning className)
        {
            throw new System.NotImplementedException();
        }
    }
}