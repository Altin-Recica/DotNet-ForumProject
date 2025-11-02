using Forum.BL;
using Forum.BL.Domain;
using Forum.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMessageController : ControllerBase
{
    private readonly IManager _mgr;

    public UserMessageController(IManager manager)
    {
        _mgr = manager;
    }

    [HttpGet("Messages")]
    public IEnumerable<Message> GetAllMessages()
    {
        return _mgr.GetMessages();
    }
    
    [HttpGet("NonUserMessages/{id}")]
    public IEnumerable<Message> GetAllNonUserMessages(int id)
    {
        return _mgr.GetNonUserMessages(id);
    }

    [HttpPost]
    [Route("CreateUserMessage")]
    public ActionResult<UserMessage> CreateUserMessage(NewMessageDto newMessageDto)
    {
        User user = _mgr.GetUser(newMessageDto.UserId);
        Message message = _mgr.GetMessage(newMessageDto.MessageId);
        DateOnly date = newMessageDto.InteractieDatum;
        if (date == new DateOnly(0001, 01, 01))
        {
            DateOnly.FromDateTime(DateTime.Now);
        }
        UserMessage userMessage = _mgr.AddUserMessage(user, message, DateOnly.FromDateTime(DateTime.Now));

        return CreatedAtAction("GetUserMessage", "User",
            new { userId = userMessage.User.Id, messageId = userMessage.Message.Id },
            new NewMessageDto
            {
                UserId = userMessage.User.Id,
                MessageId = userMessage.Message.Id,
                InteractieDatum = userMessage.InteractionDate
            }
        );
    }

}