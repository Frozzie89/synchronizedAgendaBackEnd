using System.Collections.Generic;

namespace TI_BackEnd.Domain.Event
{
    public interface IEventRepository : IRepositoryGet<Event>, IRepositoryCreate<Event>, IRepositoryDelete<Event>, IRepositoryUpdate<Event>
    {
        bool DeleteAllFromPlanning(int idPlanning);
        IEnumerable<Event> QueryFromPlanning(int idPlanning);
    }
}