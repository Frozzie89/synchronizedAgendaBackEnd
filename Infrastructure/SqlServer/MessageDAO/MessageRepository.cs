using System.Collections.Generic;
using TI_BackEnd.Domain.Message;

namespace TI_BackEnd.Infrastructure.SqlServer.MessageDAO
{
    public class MessageRepository : IMessageRepository
    {
        public Message Create(Message className)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Message Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> Query()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> QueryChat()
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, Message className)
        {
            throw new System.NotImplementedException();
        }
    }
}