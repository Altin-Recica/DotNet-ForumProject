using Forum.BL;
using Forum.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IManager _mgr;

    public CommentsController(IManager mgr)
    {
        _mgr = mgr;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Comment>> GetComments()
    {
        IEnumerable<Comment> comments = _mgr.GetComments();
        return Ok(comments);
    }

    [HttpPost]
    public ActionResult<Comment> CreateComment(Comment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _mgr.AddComment(comment.Text, comment.Upvotes, comment.Downvotes); 
            return CreatedAtAction(nameof(GetComments), new { id = comment.Id }, comment);
        }
        catch (Exception)
        {
            return BadRequest("Failed to create the comment.");
        }
    }
}