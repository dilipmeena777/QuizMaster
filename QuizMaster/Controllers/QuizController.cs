using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

public class QuizController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuizController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 🔹 Quiz Start
    public IActionResult Start()
    {
        var questions = _context.Questions.ToList();
        return View(questions);
    }

    // 🔹 Quiz Submit
    [HttpPost]
    public IActionResult Submit(List<string> answers)
    {
        var questions = _context.Questions.ToList();
        int score = 0;

        if (answers == null)
        {
            answers = new List<string>();
        }

        for (int i = 0; i < questions.Count; i++)
        {
            if (i < answers.Count)
            {
                var userAnswer = answers[i];
                var correctAnswer = questions[i].CorrectOption;

                if (!string.IsNullOrEmpty(userAnswer) &&
                    userAnswer.Trim().ToUpper() == correctAnswer.Trim().ToUpper())
                {
                    score++;
                }
            }
        }

        ViewBag.Score = score;
        return View("Result");
    }
}