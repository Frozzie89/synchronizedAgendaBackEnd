using System.Collections.Generic;

namespace TI_BackEnd.Domain.Event
{
    public interface IEventRepository : IRepository<Event>
    {
        bool DeleteAllFromPlanning(int idPlanning);
        IEnumerable<Event> QueryFromPlanning(int idPlanning);
    }
}