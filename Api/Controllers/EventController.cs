using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Event;
using TI_BackEnd.Infrastructure.SqlServer.EventDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private IEventRepository _eventRepository = new EventRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Event>> Query()
        {
            return Ok(_eventRepository.Query().Cast<Event>());
        }

        [HttpGet]
        [Route("{idPlanning:int}/*")]
        public ActionResult<IEnumerable<Event>> QueryFromPlanning(int idPlanning)
        {
            return Ok(_eventRepository.QueryFromPlanning(idPlanning).Cast<Event>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Event> Get(int id)
        {
            Event eventt = _eventRepository.Get(id);
            return eventt != null ? (ActionResult<Event>)Ok(eventt) : NotFound();
        }

        [HttpPost]
        public ActionResult<Event> Create([FromBody] Event eventt)
        {
            return Ok(_eventRepository.Create(eventt));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_eventRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put(int id, [FromBody] Event eventt)
        {
            if (_eventRepository.Update(id, eventt))
                return Ok();

            return NotFound();
        }

    }
}