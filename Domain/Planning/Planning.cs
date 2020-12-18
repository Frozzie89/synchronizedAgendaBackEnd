namespace TI_BackEnd.Domain.Planning
/*This class is used to define a planning.
 * A planning is defined by an id, a label and the super user id.
 * A superuser can be considered the moderator of the planning
 */
{
    public class Planning
    {
        public int Id { get; set; }
        public string LabelPlanning { get; set; }
        public int IdSuperUser { get; set; }

        public Planning() { }

        public Planning(int id, string labelPlanning, int idSuperUser)
        {
            Id = id;
            LabelPlanning = labelPlanning;
            IdSuperUser = idSuperUser;
        }
    }
}