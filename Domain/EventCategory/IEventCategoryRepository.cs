using System.Collections.Generic;

namespace TI_BackEnd.Domain.EventCategory
{
    public interface IEventCategoryRepository : IRepositoryGetOnly<EventCategory>
    {
        EventCategory GetByLabel(string label);
    }
}