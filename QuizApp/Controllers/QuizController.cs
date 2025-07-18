using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Enteties;
using QuizApp.Models.Viewmodel;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext dbContext;
       

        public QuizController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questions = dbContext.Questions.Include(x => x.Options)
                .Select(x => new QuestionItem()
              {
                  Id = x.Id,
                  Text = x.Text,
                  Opions = x.Options.Select(y => new SelectListItem(y.Text, y.Id.ToString())).ToList()
              })
                  .ToList();
            return View(new QuizViewMdel() { Questions = questions });
        }

        [HttpPost]
        public IActionResult Submit(List<Guid> userAnswers)
        {
            var questions = dbContext.Questions.ToList();
            var score = 0;
            var totalScore = questions.Count;
            for (var i = 0; i < userAnswers.Count; i++)
            {
                if(questions[i].CorrectOption == userAnswers[i])
                {
                    score++;
                }
            }
            ViewBag.Score = score;
            ViewBag.TotalScore = totalScore;
            return View("Results");

        }
    }
}
