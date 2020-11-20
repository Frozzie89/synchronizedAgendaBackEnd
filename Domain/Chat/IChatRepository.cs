namespace TI_BackEnd.Domain.Chat
{
    public interface IChatRepository : IRepository<Chat>
    {
        bool DeleteByPlanningId(int idPlanning);
        Chat GetByPlanningId(int idPlanning);
    }
}