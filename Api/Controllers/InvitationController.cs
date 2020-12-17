using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.InvitationDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationController : ControllerBase
    {
        private InvitationRepository _invitationRepository = new InvitationRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Invitation>> Query()
        {
            return Ok(_invitationRepository.Query().Cast<Invitation>());
        }


        [HttpGet]
        [Route("{idUserRecever:int}/*")]
        public ActionResult<IEnumerable<Invitation>> QueryPlanningsOfUserRecever(int idUserRecever)
        {
            return Ok(_invitationRepository.QueryPlanningsOfUserRecever(idUserRecever).Cast<Planning>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Invitation> GetById(int id)
        {
            Invitation invitation = _invitationRepository.Get(id);
            return invitation != null ? (ActionResult<Invitation>)Ok(invitation) : NotFound();
        }

        [HttpGet]
        [Route("{idUserRecever:int}/{idPlanning:int}")]
        public ActionResult<Invitation> GetByIdUserRecever(int idUserRecever, int idPlanning)
        {
            Invitation invitation = _invitationRepository.GetByUserAndPlanning(idUserRecever, idPlanning);
            return invitation != null ? (ActionResult<Invitation>)Ok(invitation) : NotFound();
        }

        [HttpPost]
        [Route("{userEmail}")]
        public ActionResult<Invitation> Create([FromBody] Invitation invitation, string userEmail)
        {
            Invitation inv = _invitationRepository.Create(invitation, userEmail);

            if (inv == null)
                return NotFound();
            else
                return inv;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_invitationRepository.Delete(id))
                return Ok();

            return NotFound();
        }
    }
}