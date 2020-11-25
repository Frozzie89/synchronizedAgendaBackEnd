using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Domain.Member;
using TI_BackEnd.Infrastructure.SqlServer.InvitationDAO;

namespace TI_BackEnd.Infrastructure.SqlServer.MemberDAO
{
    public class MemberRepository : IMemberRepository
    {
        private IFactory<Member> _memberFactory = new MemberFactory();

        public Member Create(Member member)
        {
            IInvitationRepository _invitationRepository = new InvitationRepository();

            // interdiction de créer un membre s'il n'y a pas d'invitation en cours
            Invitation invitation = _invitationRepository.GetByUserAndPlanning(member.IdUser, member.IdPlanning);
            if (invitation == null)
                return null;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MemberQueries.ReqCreate;
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", member.IdUser);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdPlanning}", member.IdPlanning);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIsGranted}", member.IsGranted);

                command.ExecuteNonQuery();
            }

            // une fois le membre présent, plus besoin de l'invitation
            _invitationRepository.Delete(invitation.Id);

            return member;
        }

        public bool Delete(int idUser, int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MemberQueries.ReqDelete;
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", idUser);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdPlanning}", idPlanning);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public Member Get(int idUser, int idPlanning)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MemberQueries.ReqGet;
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", idUser);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdPlanning}", idPlanning);


                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                    return _memberFactory.CreateFromReader(reader);

                return null;
            }
        }

        public IEnumerable<Member> Query()
        {
            IList<Member> members = new List<Member>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MemberQueries.ReqQuery;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    members.Add(_memberFactory.CreateFromReader(reader));
            }

            return members;
        }

        public IEnumerable<Member> QueryFromGrantedUser(int idUser)
        {
            IList<Member> members = new List<Member>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MemberQueries.ReqQueryFromGrantedUser;

                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", idUser);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    members.Add(_memberFactory.CreateFromReader(reader));
            }

            return members;
        }

        public IEnumerable<Member> QueryFromPlanning(int idPlanning)
        {
            IList<Member> members = new List<Member>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MemberQueries.ReqQueryFromPlanning;

                command.Parameters.AddWithValue($"@{MemberQueries.ColIdPlanning}", idPlanning);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    members.Add(_memberFactory.CreateFromReader(reader));
            }

            return members;
        }

        public IEnumerable<Member> QueryFromUser(int idUser)
        {
            IList<Member> members = new List<Member>();
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = MemberQueries.ReqQueryFromUser;

                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", idUser);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    members.Add(_memberFactory.CreateFromReader(reader));
            }

            return members;
        }

        public bool Update(int idUser, int idPlanning, Member member)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = MemberQueries.ReqUpdate;
                command.Parameters.AddWithValue($"@{MemberQueries.ColIsGranted}", member.IsGranted);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdUser}", idUser);
                command.Parameters.AddWithValue($"@{MemberQueries.ColIdPlanning}", idPlanning);

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}