using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Show all questions
    public IActionResult Index()
    {
        var questions = _context.Questions
            .Where(q => q.QuestionText != null)
            .ToList();

        return View(questions); // 🔥 ye missing tha
    }

    // Add Question (GET)
    public IActionResult Create()
    {
        return View();
    }

    // Add Question (POST)
    [HttpPost]
    public IActionResult Create(Question q)
    {
        if (string.IsNullOrEmpty(q.QuestionText))
        {
            return View(q);
        }

        _context.Questions.Add(q);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // Delete Question
    public IActionResult Delete(int id)
    {
        var q = _context.Questions.Find(id);

        if (q != null)
        {
            _context.Questions.Remove(q);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    // Edit (GET)
    public IActionResult Edit(int id)
    {
        var q = _context.Questions.Find(id);
        return View(q);
    }

    // Edit (POST)
    [HttpPost]
    public IActionResult Edit(Question q)
    {
        if (string.IsNullOrEmpty(q.QuestionText))
        {
            return View(q);
        }

        _context.Questions.Update(q);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
    {
        var role = HttpContext.Session.GetString("UserRole");

        if (role != "Admin")
        {
            context.Result = RedirectToAction("Login", "Account");
        }

        base.OnActionExecuting(context);
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // 🔥 session clear
        return RedirectToAction("Index", "Home"); // 👉 Home page pe bhej
    }

}