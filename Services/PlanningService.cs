using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Services
{
    public class PlanningService
    {
        private ChatRepository _chatRepository = new ChatRepository();
        private UserRepository _userRepository = new UserRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();

        public PlanningService() { }

        public bool canCreate(Planning planning)
        {
            if (_userRepository.Get(planning.IdSuperUser) == null)
                return false;

            return true;
        }
    }
}