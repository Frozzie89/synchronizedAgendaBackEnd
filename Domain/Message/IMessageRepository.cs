using System.Collections.Generic;

namespace TI_BackEnd.Domain.Message
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> QueryChat();
    }
}