namespace TI_BackEnd.Domain.Invitation
{
    public class Invitation
    {
        public int Id { get; set; }
        public int IdUserRecever { get; set; }
        public int IdPlanning { get; set; }

        public Invitation() { }

        public Invitation(int id, int idUserRecever, int idPlanning)
        {
            Id = id;
            IdUserRecever = idUserRecever;
            IdPlanning = idPlanning;
        }
    }
}