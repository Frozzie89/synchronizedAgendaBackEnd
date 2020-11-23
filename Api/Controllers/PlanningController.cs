using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/plannings")]
    public class PlanningController : ControllerBase
    {
        private IPlanningRepository _planningRepository = new PlanningRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Planning>> Query()
        {
            return Ok(_planningRepository.Query().Cast<Planning>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Planning> GetById(int id)
        {
            Planning planning = _planningRepository.Get(id);
            return planning != null ? (ActionResult<Planning>)Ok(planning) : NotFound();
        }

        [HttpGet]
        [Route("{label}")]
        public ActionResult<Planning> GetByLabel(string label)
        {
            Planning planning = _planningRepository.GetByLabel(label);
            return planning != null ? (ActionResult<Planning>)Ok(planning) : NotFound();
        }

        [HttpGet]
        [Route("{idSuperUser:int}/SU")]
        public ActionResult<Planning> GetBySuperId(int idSuperUser)
        {
            Planning planning = _planningRepository.GetBySuperUser(idSuperUser);
            return planning != null ? (ActionResult<Planning>)Ok(planning) : NotFound();
        }

        [HttpPost]
        public ActionResult<Planning> Create([FromBody] Planning planning)
        {
            return Ok(_planningRepository.Create(planning));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_planningRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put(int id, [FromBody] Planning planning)
        {
            if (_planningRepository.Update(id, planning))
                return Ok();

            return NotFound();
        }
    }
}