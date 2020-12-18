namespace TI_BackEnd.Domain.Invitation
/*
 * This class is used to define an invitation
 * An invitation is defined by an id, an id from a invited user and an id from the planning where the user
 * is invited
 */
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

        public override string ToString()
        {
            return "" + Id + ' ' + IdUserRecever + ' ' + IdPlanning;
        }
    }
}