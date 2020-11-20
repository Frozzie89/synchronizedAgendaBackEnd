using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Invitation;

namespace TI_BackEnd.Infrastructure.SqlServer.InvitationDAO
{
    public class InvitationRepository : IInvitationRepository
    {
        IFactory<Invitation> _invitationFactory = new InvitationFactory();

        public Invitation Create(Invitation invitation)
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

            return invitation;
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

        public Invitation GetByUserRecever(int idUserRecever)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqGetByIdUserRecever;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", idUserRecever);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _invitationFactory.CreateFromReader(reader);

                return null;
            }
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

        public bool Update(int id, Invitation invitation)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = InvitationQueries.ReqUpdate;
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", invitation.IdUserRecever);
                command.Parameters.AddWithValue($"@{InvitationQueries.ColIdPlanning}", invitation.IdPlanning);
                command.Parameters.AddWithValue($"@{InvitationQueries.ColId}", id);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}