using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Services;

namespace TI_BackEnd.Infrastructure.SqlServer.InvitationDAO
{
    public class InvitationRepository : IInvitationRepository
    {
        IFactory<Invitation> _invitationFactory = new InvitationFactory();
        InvitatationService _invitationService = new InvitatationService();

        public Invitation Create(Invitation invitation)
        {
            if (_invitationService.canCreate(invitation))
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = InvitationQueries.ReqCreate;

                    command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", invitation.IdUserRecever);
                    command.Parameters.AddWithValue($"@{InvitationQueries.ColIdPlanning}", invitation.IdPlanning);

                    invitation.Id = (int)command.ExecuteScalar();
                }
            }

            return invitation;
        }


        public Invitation Create(Invitation invitation, string userEmail)
        {

            if (_invitationService.canCreate(invitation) && _invitationService.doesUserExist(userEmail))
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = InvitationQueries.ReqCreate;

                    command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", invitation.IdUserRecever);
                    command.Parameters.AddWithValue($"@{InvitationQueries.ColIdPlanning}", invitation.IdPlanning);

                    invitation.Id = (int)command.ExecuteScalar();
                }
            }
            return null;
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqDelete;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public Invitation Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqGetById;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColId}", id);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _invitationFactory.CreateFromReader(reader);

                return null;
            }
        }

        public Invitation GetByUserAndPlanning(int idUserRecever, int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqGetByUserReceverAndPlanning;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", idUserRecever);
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdPlanning}", idPlanning);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _invitationFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Planning> QueryPlanningsOfUserRecever(int idUserRecever)
        {
            IFactory<Planning> _planningFactory = new PlanningFactory();
            IList<Planning> invitedPlannings = new List<Planning>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqGetPlanningsOfUserRecever;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", idUserRecever);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    invitedPlannings.Add(_planningFactory.CreateFromReader(reader));
            }

            return invitedPlannings;
        }

        public IEnumerable<Invitation> Query()
        {
            IList<Invitation> invitations = new List<Invitation>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = InvitationQueries.ReqQuery;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    invitations.Add(_invitationFactory.CreateFromReader(reader));
            }

            return invitations;
        }

    }
}