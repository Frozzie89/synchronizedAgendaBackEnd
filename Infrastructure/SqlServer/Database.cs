using System.Data.SqlClient;

namespace TI_BackEnd.Infrastructure.SqlServer
{
    public class Database
    {
        private static readonly string ConnectionString = @"Server=MSI\SQLEXPRESS;Database=todos;Integrated Security=SSPI";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}