namespace TI_BackEnd.Domain.Planning
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