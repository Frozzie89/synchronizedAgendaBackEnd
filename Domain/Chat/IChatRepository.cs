namespace TI_BackEnd.Domain.Chat
{
    public interface IChatRepository : IRepositoryGet<Chat>, IRepositoryCreate<Chat>
    {
        bool DeleteByPlanningId(int idPlanning);
        Chat GetByPlanningId(int idPlanning);
    }
}