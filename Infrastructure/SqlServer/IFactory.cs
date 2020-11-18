using System.Data.SqlClient;

namespace TI_BackEnd.Infrastructure.SqlServer
{
    public interface IFactory<T>
    {
        T CreateFromReader(SqlDataReader reader);
    }
}