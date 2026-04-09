using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Register Page (GET)
    public IActionResult Register()
    {
        return View();
    }

    // ✅ Register (POST)
    [HttpPost]
    public IActionResult Register(User user)
    {
        user.Role = "User"; // default role

        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Login");
    }

    // ✅ Login Page (GET)
    public IActionResult Login()
    {
        return View();
    }

    // ✅ Login (POST)
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            // 🔥 Session set
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Start", "Quiz");
            }
        }

        ViewBag.Message = "Invalid Login";
        return View();
    }
}