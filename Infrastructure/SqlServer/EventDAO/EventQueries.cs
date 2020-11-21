namespace TI_BackEnd.Infrastructure.SqlServer.EventDAO
{
    public static class EventQueries
    {
        public static readonly string TableName = "Event";
        public static readonly string ColId = "idEvent";
        public static readonly string ColIdEventCategory = "idEventCategory";
        public static readonly string ColIdPlanning = "idPlanning";
        public static readonly string ColLabel = "labelEvent";
        public static readonly string ColStart = "startEvent";
        public static readonly string ColEnd = "endEvent";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqQueryFromPlanning = $@"
            SELECT * FROM [{TableName}]
            WHERE {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColIdEventCategory}), ({ColIdPlanning}), ({ColLabel}), ({ColStart}), ({ColEnd})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdEventCategory}, @{ColIdPlanning}, @{ColLabel}, @{ColStart}, @{ColEnd})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqDeleteFromPlanning = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId} AND {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColLabel} = @{ColLabel},
            {ColStart} = @{ColStart},
            {ColEnd} = @{ColEnd},
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGet = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";
    }
}