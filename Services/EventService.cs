using TI_BackEnd.Domain.Event;
using TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO;
using TI_BackEnd.Infrastructure.SqlServer.EventDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;

namespace TI_BackEnd.Service
{
    public class EventService
    {
        private EventCategoryRepository _eventCategoryRepository = new EventCategoryRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();
        private EventRepository _eventRepository = new EventRepository();

        public EventService() { }

        public bool canCreate(Event eventt)
        {
            // interdiction de créer un évenement avec une catégorie qui n'existe pas
            if (_eventCategoryRepository.Get(eventt.IdEventCategory) == null)
                return false;

            // interdiction de crééer un évenement dans un planning qui n'existe pas
            if (_planningRepository.Get(eventt.IdPlanning) == null)
                return false;

            return true;
        }
    }
}