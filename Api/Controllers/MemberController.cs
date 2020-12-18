using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Member;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.MemberDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : ControllerBase
    {
        private IMemberRepository _memberRepository = new MemberRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Member>> Query()
        {
            return Ok(_memberRepository.Query().Cast<Member>());
        }

        [HttpGet]
        [Route("{idPlanning:int}/p")]
        public ActionResult<IEnumerable<Member>> QueryFromPlanning(int idPlanning)
        {
            return Ok(_memberRepository.QueryFromPlanning(idPlanning).Cast<Member>());
        }

        [HttpGet]
        [Route("{idUser:int}")]
        public ActionResult<IEnumerable<Planning>> QueryPlanningsFromMember(int idUser)
        {
            return Ok(_memberRepository.QueryPlanningsFromMember(idUser, false).Cast<Planning>());
        }

        [HttpGet]
        [Route("{idUser:int}")]
        public ActionResult<IEnumerable<Planning>> QueryPlanningsFromGrantedMember(int idUser)
        {
            return Ok(_memberRepository.QueryPlanningsFromMember(idUser, true).Cast<Planning>());
        }

        [HttpGet]
        [Route("{idUser:int}")]
        public ActionResult<IEnumerable<Member>> QueryFromGrantedUser(int idUser)
        {
            return Ok(_memberRepository.QueryFromGrantedUser(idUser).Cast<Member>());
        }

        [HttpGet]
        [Route("{idUser:int}/{idPlanning:int}")]
        public ActionResult<Member> Get(int idUser, int idPlanning)
        {
            Member member = _memberRepository.Get(idUser, idPlanning);
            return member != null ? (ActionResult<Member>)Ok(member) : NotFound();
        }

        [HttpPost]
        public ActionResult<Member> Create([FromBody] Member member)
        {
            return Ok(_memberRepository.Create(member));
        }

        [HttpDelete]
        [Route("{idUser:int}/{idPlanning:int}")]
        public ActionResult Delete(int idUser, int idPlanning)
        {
            if (_memberRepository.Delete(idUser, idPlanning))
                return Ok();

            return NotFound();
        }


        [HttpPut]
        [Route("{idUser:int}/{idPlanning:int}")]
        public ActionResult Put(int idUser, int idPlanning, [FromBody] Member member)
        {
            if (_memberRepository.Update(idUser, idPlanning, member))
                return Ok();

            return NotFound();
        }

    }
}