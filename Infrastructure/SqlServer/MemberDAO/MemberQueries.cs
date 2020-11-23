namespace TI_BackEnd.Infrastructure.SqlServer.MemberDAO
{
    public static class MemberQueries
    {
        public static readonly string TableName = "Member";
        public static readonly string ColIdUser = "idUser";
        public static readonly string ColIdPlanning = "idPlanning";
        public static readonly string ColIsGranted = "isGranted";

        public static readonly string ReqQuery = $"SELECT * FROM [{TableName}]";

        public static readonly string ReqQueryFromPlanning = $@"
            SELECT * FROM [{TableName}]
            WHERE {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqQueryFromUser = $@"
            SELECT * FROM [{TableName}]
            WHERE {ColIdUser} = @{ColIdUser}";

        public static readonly string ReqCreate = $@"
            INSERT INTO [{TableName}]({ColIdUser}, {ColIdPlanning}, {ColIsGranted})
            VALUES(@{ColIdUser}, @{ColIdPlanning}, @{ColIsGranted})";

        public static readonly string ReqDelete = $@"
            DELETE FROM [{TableName}]
            WHERE {ColIdUser} = @{ColIdUser} AND {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqUpdate = $@"
            UPDATE [{TableName}]
            SET
            {ColIsGranted} = @{ColIsGranted},
            WHERE {ColIdUser} = @{ColIdUser} AND {ColIdPlanning} = @{ColIdPlanning}";

        public static readonly string ReqGet = ReqQuery + $@"
            WHERE {ColIdUser} = @{ColIdUser} AND {ColIdPlanning} = @{ColIdPlanning}";
    }
}