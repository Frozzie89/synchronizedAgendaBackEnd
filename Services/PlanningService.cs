using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Services
{
    public class PlanningService
    {
        public PlanningService() { }

        public bool canCreate(Planning planning)
        {
            ChatRepository _chatRepository = new ChatRepository();
            UserRepository _userRepository = new UserRepository();
            PlanningRepository _planningRepository = new PlanningRepository();

            if (_userRepository.Get(planning.IdSuperUser) == null)
                return false;

            return true;
        }
    }
}