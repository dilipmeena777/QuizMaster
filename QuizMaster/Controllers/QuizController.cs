using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

    // 🔹 Quiz Submit (FINAL FIXED)
    [HttpPost]
    public IActionResult Submit(List<string> answers, List<int> questionIds)
    {
        int score = 0;

        if (answers == null)
        {
            answers = new List<string>();
        }

        for (int i = 0; i < questionIds.Count; i++)
        {
            var question = _context.Questions.Find(questionIds[i]);

            if (question != null && i < answers.Count)
            {
                var userAnswer = answers[i];
                var correctAnswer = question.CorrectOption;

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