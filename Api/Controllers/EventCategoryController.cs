using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain;
using TI_BackEnd.Domain.EventCategory;
using TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/eventcategories")]
    public class EventCategoryController : ControllerBase
    {
        private IEventCategoryRepository _eventCategoryRepository = new EventCategoryRepository();

        [HttpGet]
        public ActionResult<IEnumerable<EventCategory>> Query()
        {
            return Ok(_eventCategoryRepository.Query().Cast<EventCategory>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<EventCategory> GetById(int id)
        {
            EventCategory eventCategory = _eventCategoryRepository.Get(id);
            return eventCategory != null ? (ActionResult<EventCategory>)Ok(eventCategory) : NotFound();
        }

        [HttpGet]
        [Route("{label}")]
        public ActionResult<EventCategory> GetByLabel(string label)
        {
            EventCategory eventCategory = _eventCategoryRepository.GetByLabel(label);
            return eventCategory != null ? (ActionResult<EventCategory>)Ok(eventCategory) : NotFound();
        }
    }
}