namespace TI_BackEnd.Domain.Planning
{
    public interface IPlanningRepository : IRepositoryGet<Planning>, IRepositoryCreate<Planning>
    {
        Planning GetByLabel(string LabelPlanning);
        Planning GetBySuperUser(int IdSuperUser);
    }
}