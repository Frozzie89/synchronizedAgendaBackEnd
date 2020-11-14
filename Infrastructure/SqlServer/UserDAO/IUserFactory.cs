using System.Data.SqlClient;
using TI_BackEnd.Domain.User;

namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public interface IUserFactory
    {
        User CreateFromReader(SqlDataReader reader);
    }
}