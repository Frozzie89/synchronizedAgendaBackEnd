namespace TI_BackEnd.Domain.Chat
{
    public class Chat
    {
        public int Id { get; set; }
        public int IdPlanning { get; set; }

        public Chat() { }

        public Chat(int id, int idPlanning)
        {
            Id = id;
            IdPlanning = idPlanning;
        }
    }
}