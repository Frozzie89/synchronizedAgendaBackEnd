namespace TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO
{
    public static class EventCategoryQueries
    {
        public static readonly string TableName = "EventCategory";
        public static readonly string ColId = "idEventCategory";
        public static readonly string ColLabel = "labelCat";
        public static readonly string ColColor = "color";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColLabel}, {ColColor})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColLabel}, @{ColColor})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColLabel} = @{ColLabel},
            {ColColor} = @{ColColor},
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetById = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByLabel = ReqQuery + $@"
            WHERE {ColLabel} = @{ColLabel}";

    }
}