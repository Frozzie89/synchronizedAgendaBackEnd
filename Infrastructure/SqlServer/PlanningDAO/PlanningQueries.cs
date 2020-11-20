namespace TI_BackEnd.Infrastructure.SqlServer.PlanningDAO
{
    public static class PlanningQueries
    {
        public static readonly string TableName = "Planning";
        public static readonly string ColId = "idPlanning";
        public static readonly string ColLabel = "labelPlanning";
        public static readonly string ColIdSuperUser = "superUser";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColLabel}, {ColIdSuperUser}
            OUTPUT INSERTED.{ColId})
            VALUES(@{ColLabel}, @{ColIdSuperUser})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColLabel} = @{ColLabel}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetById = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByLabel = ReqQuery + $@"
            WHERE {ColLabel} = @{ColLabel}";

        public static readonly string ReqGetBySuperUser = ReqQuery + $@"
            WHERE {ColIdSuperUser} = @{ColIdSuperUser}";
    }
}