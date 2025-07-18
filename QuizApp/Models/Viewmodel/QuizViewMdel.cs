using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuizApp.Models.Viewmodel
{
    public class QuizViewMdel
    {
        public required List<QuestionItem> Questions { get; set; } = []; 
    }
    public class QuestionItem
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required List<SelectListItem> Opions { get; set; }
    }
}
