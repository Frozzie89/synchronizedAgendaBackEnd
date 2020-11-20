using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Message;
using TI_BackEnd.Infrastructure.SqlServer.MessageDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _messageRepository = new MessageRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Message>> Query()
        {
            return Ok(_messageRepository.Query().Cast<Message>());
        }

        [HttpGet]
        [Route("{idChat:int}/*")]
        public ActionResult<IEnumerable<Message>> QueryFromChat(int idChat)
        {
            return Ok(_messageRepository.QueryFromChat(idChat).Cast<Message>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Message> Get(int id)
        {
            Message message = _messageRepository.Get(id);
            return message != null ? (ActionResult<Message>)Ok(message) : NotFound();
        }

        [HttpPost]
        public ActionResult<Message> Create([FromBody] Message message)
        {
            return Ok(_messageRepository.Create(message));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_messageRepository.Delete(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put(int id, [FromBody] Message message)
        {
            if (_messageRepository.Update(id, message))
                return Ok();

            return NotFound();
        }
    }
}