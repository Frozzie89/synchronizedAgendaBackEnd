using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.InvitationDAO;
using TI_BackEnd.Infrastructure.SqlServer.MemberDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Services
{
    public class InvitatationService
    {
        private MemberRepository _memberRepository = new MemberRepository();
        private UserRepository _userRepository = new UserRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();
        private InvitationRepository _invitationRepository = new InvitationRepository();

        public InvitatationService() { }

        public bool canCreate(Invitation invitation)
        {
            // interdiction d'inviter le même utilisateur pour le même planning
            if (_invitationRepository.GetByUserAndPlanning(invitation.IdUserRecever, invitation.IdUserRecever) != null)
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

        public bool doesUserExist(string userEmail)
        {
            return _userRepository.Get(userEmail) != null;
        }
    }
}