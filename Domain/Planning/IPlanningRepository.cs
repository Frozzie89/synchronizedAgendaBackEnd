namespace TI_BackEnd.Domain.Planning
{
    public interface IPlanningRepository : IRepository<Planning>
    {
        Planning GetByLabel(string LabelPlanning);
        Planning GetBySuperUser(int IdSuperUser);
    }
}