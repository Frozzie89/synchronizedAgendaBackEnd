using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository = new UserRepository();

        [HttpGet]
        public ActionResult<IEnumerable<User>> Query()
        {
            return Ok(_userRepository.Query().Cast<User>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            User user = _userRepository.Get(id);
            return user != null ? (ActionResult<User>)Ok(user) : NotFound();
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            return Ok(_userRepository.Create(user));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (_userRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            if (_userRepository.Update(id, user))
                return Ok();

            return NotFound();
        }
    }
}