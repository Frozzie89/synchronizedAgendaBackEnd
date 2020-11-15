using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Route("{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            User user = _userRepository.Get(email);
            return user != null ? (ActionResult<User>)Ok(user) : NotFound();
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public ActionResult<User> GetAuthentication(string email, string password)
        {
            User user = _userRepository.GetAuthentication(email, password);

            if (user != null)
                return Ok(new { User = new UserAuthResponse(user) });

            return Unauthorized();
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            return Ok(_userRepository.Create(user));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_userRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpDelete]
        [Route("{email}")]
        public ActionResult Delete(string email)
        {
            if (_userRepository.Delete(email))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            if (_userRepository.Update(id, user))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{email}")]
        public ActionResult Put(string email, [FromBody] User user)
        {
            if (_userRepository.Update(email, user))
                return Ok();

            return NotFound();
        }
    }
}