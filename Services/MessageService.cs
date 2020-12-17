using TI_BackEnd.Domain.Message;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Infrastructure.SqlServer.MessageDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Services
{
    public class MessageService
    {
        private UserRepository _userRepository = new UserRepository();
        private ChatRepository _chatRepository = new ChatRepository();
        private MessageRepository _messageRepository = new MessageRepository();

        public MessageService() { }

        public bool canCreate(Message message)
        {
            // interdiction de créer un message si pas d'utilisateur ou pas de chat
            if (_userRepository.Get(message.IdUser) == null || _chatRepository.Get(message.IdChat) == null)
                return false;

            return true;
        }
    }
}