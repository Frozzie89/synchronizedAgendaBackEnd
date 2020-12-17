using System.Collections.Generic;

namespace TI_BackEnd.Domain.Message
{
    public interface IMessageRepository : IRepositoryGet<Message>, IRepositoryCreate<Message>
    {
        IEnumerable<Message> QueryFromChat(int idChat);
        bool DeleteAllFromChat(int idChat);
    }
}