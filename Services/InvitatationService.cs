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
        public InvitatationService() { }

        public bool canCreate(Invitation invitation)
        {
            MemberRepository _memberRepository = new MemberRepository();
            UserRepository _userRepository = new UserRepository();
            PlanningRepository _planningRepository = new PlanningRepository();
            InvitationRepository _invitationRepository = new InvitationRepository();

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
            UserRepository _userRepository = new UserRepository();
            return _userRepository.Get(userEmail) != null;
        }
    }
}