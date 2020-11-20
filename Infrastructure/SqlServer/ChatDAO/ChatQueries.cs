namespace TI_BackEnd.Infrastructure.SqlServer.ChatDAO
{
    public static class ChatQueries
    {
        public static readonly string TableName = "Chat";
        public static readonly string ColId = "idChat";
        public static readonly string ColIdPlanning = "idPlanning";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColIdPlanning})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdPlanning})";

        public static readonly string ReqDeleteById = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqDeleteByPlanningId = $@"
            DELETE FROM [{TableName}]
            WHERE {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColIdPlanning} = @{ColIdPlanning}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetById = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByIdPlanning = ReqQuery + $@"
            WHERE {ColIdPlanning} = @{ColIdPlanning}";
    }
}