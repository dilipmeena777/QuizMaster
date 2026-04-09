using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;

namespace QuizMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor update
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // 🔥 Dashboard Method Added
        public IActionResult Dashboard()
        {
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalQuestions = _context.Questions.Count();
            ViewBag.TotalAttempts = _context.Results.Count();
            ViewBag.TopScore = _context.Results.Max(r => (int?)r.Score) ?? 0;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Leaderboard()
        {
            var topUsers = _context.Results
                            .OrderByDescending(r => r.Score)
                            .Take(10)
                            .ToList();

            return View(topUsers);
        }
    }
}