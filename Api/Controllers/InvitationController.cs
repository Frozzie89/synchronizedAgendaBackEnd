using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Invitation;
using TI_BackEnd.Infrastructure.SqlServer.InvitationDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationController : ControllerBase
    {
        private IInvitationRepository _invitationRepository = new InvitationRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Invitation>> Query()
        {
            return Ok(_invitationRepository.Query().Cast<Invitation>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Invitation> GetById(int id)
        {
            Invitation invitation = _invitationRepository.Get(id);
            return invitation != null ? (ActionResult<Invitation>)Ok(invitation) : NotFound();

        }
        [HttpGet]
        [Route("{idUserRecever:int}")]
        public ActionResult<Invitation> GetByIdUserRecever(int idUserRecever)
        {
            Invitation invitation = _invitationRepository.GetByUserRecever(idUserRecever);
            return invitation != null ? (ActionResult<Invitation>)Ok(invitation) : NotFound();
        }

        [HttpPost]
        public ActionResult<Invitation> Create([FromBody] Invitation invitation)
        {
            return Ok(_invitationRepository.Create(invitation));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_invitationRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put(int id, [FromBody] Invitation invitation)
        {
            if (_invitationRepository.Update(id, invitation))
                return Ok();

            return NotFound();
        }
    }
}