namespace TI_BackEnd.Infrastructure.SqlServer.MessageDAO
{
    public static class MessageQueries
    {
        public static readonly string TableName = "Message";
        public static readonly string ColId = "idMessage";
        public static readonly string ColIdChat = "idChat";
        public static readonly string ColIdUser = "idUser";
        public static readonly string ColBody = "body";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqQueryFromChat = $@"
            SELECT * FROM [{TableName}]
            WHERE ({ColIdChat} = @{ColIdChat})
            ";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColIdChat}, {ColIdUser}, {ColBody})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdChat}, @{ColIdUser}, @{ColBody})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqDeleteAllFromChat = $@"
            DELETE FROM [{TableName}]
            WHERE {ColIdChat} = @{ColIdChat}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColBody} = @{ColBody}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGet = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";

    }
}