using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Domain.Member;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.MemberDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Infrastructure.SqlServer.InvitationDAO
{
    public class InvitationRepository : IInvitationRepository
    {
        IFactory<Invitation> _invitationFactory = new InvitationFactory();
        IMemberRepository _memberRepository = new MemberRepository();
        IUserRepository _userRepository = new UserRepository();
        IPlanningRepository _planningRepository = new PlanningRepository();

        private bool canCreate(Invitation invitation)
        {
            // interdiction d'inviter le même utilisateur pour le même planning
            if (GetByUserAndPlanning(invitation.IdUserRecever, invitation.IdUserRecever) != null)
                return false;

            // interdiction d'inviter un utilisateur déjà existant dans le planning
            if (_memberRepository.Get(invitation.IdUserRecever, invitation.IdPlanning) != null)
                return false;

            // interdiction d'inviter un utilisateur qui n'existe pas 
            if (_userRepository.Get(invitation.IdUserRecever) == null)
                return false;

            // interdiction d'inviter un superUtilisateur à son propre planning
            if (_planningRepository.GetBySuperUser(invitation.IdUserRecever) != null)
                return false;

            // interdiction d'inviter un utilisateur vers un planning qui n'existe pas
            if (_planningRepository.Get(invitation.IdPlanning) == null)
                return false;

            return true;
        }

        public Invitation Create(Invitation invitation)
        {
            if (canCreate(invitation))
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
            if (_userRepository.Get(userEmail) != null)
                return null;

            if (canCreate(invitation))
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

        // public IEnumerable<Invitation> QueryFromUserRecever(int idUserRecever)
        // {
        //     IList<Invitation> invitations = new List<Invitation>();
        //     using (SqlConnection connection = Database.GetConnection())
        //     {
        //         connection.Open();
        //         SqlCommand command = connection.CreateCommand();
        //         command.CommandText = InvitationQueries.ReqQueryFromUserRecever;
        //         command.Parameters.AddWithValue($"@{InvitationQueries.ColIdUserRecever}", idUserRecever);
        //         SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //         while (reader.Read())
        //             invitations.Add(_invitationFactory.CreateFromReader(reader));
        //     }

        //     return invitations;
        // }

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