using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;

namespace TI_BackEnd.Infrastructure.SqlServer.InvitationDAO
{
    public static class InvitationQueries
    {
        public static readonly string TableName = "Invitation";
        public static readonly string ColId = "idInvitation";
        public static readonly string ColIdUserRecever = "idUserRecever";
        public static readonly string ColIdPlanning = "idPlanning";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqQueryFromUserRecever = $@"
            SELECT * FROM [{TableName}] 
            WHERE {ColIdUserRecever} = @{ColIdUserRecever}";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColIdUserRecever}, {ColIdPlanning})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdUserRecever}, @{ColIdPlanning})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColIdUserRecever} = @{ColIdUserRecever},
            {ColIdPlanning} = @{ColIdPlanning},
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetById = ReqQuery + $@"
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByUserReceverAndPlanning = ReqQuery + $@"
            WHERE {ColIdUserRecever} = @{ColIdUserRecever} AND {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqGetPlanningsOfUserRecever = $@"
            SELECT {PlanningQueries.TableName}.* FROM {TableName}
            INNER JOIN {PlanningQueries.TableName} 
            ON {TableName}.{ColIdPlanning} = {PlanningQueries.TableName}.{PlanningQueries.ColId} 
            WHERE {TableName}.{ColIdUserRecever} = @{ColIdUserRecever}";
    }
}