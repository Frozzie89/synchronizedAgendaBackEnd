using System.Data.SqlClient;
using TI_BackEnd.Domain.Invitation;

namespace TI_BackEnd.Infrastructure.SqlServer.InvitationDAO
{
    public class InvitationFactory : IFactory<Invitation>
    {
        public Invitation CreateFromReader(SqlDataReader reader)
        {
            return new Invitation
            {
                IdUserRecever = reader.GetInt32(reader.GetOrdinal(InvitationQueries.ColIdUserRecever)),
                IdPlanning = reader.GetInt32(reader.GetOrdinal(InvitationQueries.ColIdPlanning))
            };
        }
    }
}