using Forum.BL;
using Forum.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

public class MessageController : Controller
{
    private readonly IManager _mgr;

    public MessageController(IManager mgr)
    {
        _mgr = mgr;
    }

    public IActionResult Details(int id)
    {
        Message message = _mgr.GetMessage(id);
        return View(message);
    }
}