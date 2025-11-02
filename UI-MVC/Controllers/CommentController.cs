using Forum.BL;
using Forum.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

public class CommentController : Controller
{
    private readonly IManager _mgr;
    public CommentController(IManager manager)
    {
        _mgr = manager;
    }
    
    public ActionResult Index()
    {
        IEnumerable<Comment> books = _mgr.GetComments();

        return View(books);

    }
    
}