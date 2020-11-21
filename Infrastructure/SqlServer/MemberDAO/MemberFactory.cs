using System.Data.SqlClient;
using TI_BackEnd.Domain.Member;

namespace TI_BackEnd.Infrastructure.SqlServer.MemberDAO
{
    public class MemberFactory : IFactory<Member>
    {
        public Member CreateFromReader(SqlDataReader reader)
        {
            return new Member
            {
                IdUser = reader.GetInt32(reader.GetOrdinal(MemberQueries.ColIdUser)),
                IdPlanning = reader.GetInt32(reader.GetOrdinal(MemberQueries.ColIdPlanning)),
                IsGranted = reader.GetBoolean(reader.GetOrdinal(MemberQueries.ColIsGranted))
            };
        }
    }
}