using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TI_BackEnd.Domain.Chat;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;

namespace TI_BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public class ChatController : ControllerBase
    {
        private IChatRepository _chatRepository = new ChatRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Chat>> Query()
        {
            return Ok(_chatRepository.Query().Cast<Chat>());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Chat> GetById(int id)
        {
            Chat chat = _chatRepository.Get(id);
            return chat != null ? (ActionResult<Chat>)Ok(chat) : NotFound();
        }

        [HttpGet]
        [Route("{idPlanning:int}")]
        public ActionResult<Chat> GetByLabel(int idPlanning)
        {
            Chat chat = _chatRepository.GetByPlanningId(idPlanning);
            return chat != null ? (ActionResult<Chat>)Ok(chat) : NotFound();
        }
    }
}