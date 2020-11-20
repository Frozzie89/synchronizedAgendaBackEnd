namespace TI_BackEnd.Domain.Message
{
    public class Message
    {
        public int Id { get; set; }
        public int IdChat { get; set; }
        public int IdUser { get; set; }
        public string Body { get; set; }

        public Message() { }

        public Message(int id, int idChat, int idUser, string body)
        {
            Id = id;
            IdChat = idChat;
            IdUser = idUser;
            Body = body;
        }
    }
}