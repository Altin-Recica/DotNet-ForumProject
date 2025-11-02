using Forum.BL;
using Forum.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IManager _mgr;

    public MessagesController(IManager mgr)
    {
        _mgr = mgr;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Message>> GetMessages()
    {
        IEnumerable<Message> messages = _mgr.GetMessages();
        return Ok(messages);
    }

    [HttpGet("User/{userId}")]
    public ActionResult<IEnumerable<Message>> GetMessagesByUserId(int userId)
    {
        IEnumerable<Message> messages = _mgr.GetUserMessages(userId);
        return Ok(messages);
    }

    [HttpPost]
    public ActionResult<Message> CreateMessage(Message message)
    {
        _mgr.AddMessage(message.Content, message.Type, message.MessageDate);
        return CreatedAtAction("GetMessages", new { id = message.Id }, message);
    }
}
