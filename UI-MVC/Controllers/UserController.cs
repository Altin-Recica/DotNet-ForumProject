using Forum.BL;
using Forum.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.UI.MVC.Controllers;

public class UserController : Controller
{
    private readonly IManager _mgr;

    public UserController(IManager mgr)
    {
        _mgr = mgr;
    }

    public IActionResult Index()
    {
        List<User> users = _mgr.GetUsers().ToList();
        
        return View(users);
    }
    
    [HttpGet("api/User/Details/{userId}")]
    public IEnumerable<Message> GetUserMessage(int userId)
    {
        IEnumerable<Message> messages = _mgr.GetUserMessages(userId);
        return messages;
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Add(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }
        
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - user.DateOfBirth.Year;
    
        if (today < user.DateOfBirth.AddYears(age))
        {
            age--;
        }
        
        User newUser = _mgr.AddUser(user.Name,user.DateOfBirth,age);
        return RedirectToAction("Details", new { newUser.Id });
    }
    
    
    public IActionResult Details(int id)
    {
        User user = _mgr.GetUser(id);
        return View(user);
    }
    
}