using System.Collections.Generic;

namespace TI_BackEnd.Domain.EventCategory
{
    public interface IEventCategoryRepository : IRepositoryGet<EventCategory>
    {
        EventCategory GetByLabel(string label);
    }
}