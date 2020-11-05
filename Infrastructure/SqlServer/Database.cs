using System.Data.SqlClient;

namespace TI_BackEnd.Infrastructure.SqlServer
{
    public class Database
    {
        private static readonly string ConnectionString = "Server=DESKTOP-MHOOABJ\\SQLEXPRESS;Database=Project_GroupeB4;Integrated Security=SSPI";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}