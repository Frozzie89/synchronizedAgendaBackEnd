namespace TI_BackEnd.Infrastructure.SqlServer.UserDAO
{
    public static class UserQueries
    {

        public static readonly string TableName = "User";
        public static readonly string ColId = "idUser";
        public static readonly string ColEmail = "email";
        public static readonly string ColLastName = "lastname";
        public static readonly string ColFirstName = "firstname";
        public static readonly string ColUserName = "username";
        public static readonly string ColPassword = "password";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";
        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColEmail}, {ColLastName}, {ColFirstName}, {ColUserName}, {ColPassword})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColEmail}, @{ColLastName}, @{ColFirstName}, @{ColUserName}, @{ColPassword})";


        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColEmail} = @{ColEmail}";

        public static readonly string ReqUpdateById = $@"
            UPDATE [{TableName}]
            SET 
            {ColEmail} = @{ColEmail},
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColUserName} = @{ColUserName},
            {ColPassword} = @{ColPassword}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdateByEmail = $@"
            UPDATE [{TableName}]
            SET 
            {ColEmail} = @{ColEmail},
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColUserName} = @{ColUserName},
            {ColPassword} = @{ColPassword}
            WHERE {ColEmail} = @{ColEmail}";

        public static readonly string ReqGetById = ReqQuery + $@" WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByEmail = ReqQuery + $@" WHERE {ColEmail} = @{ColEmail}";

        public static readonly string ReqAuthentication = ReqGetByEmail + $@" AND  {ColPassword} = @{ColPassword}";
    }
}