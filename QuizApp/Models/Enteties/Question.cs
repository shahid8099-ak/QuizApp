namespace QuizApp.Models.Enteties
{
    public class Question
    {
        public Guid Id { get; init; }
        public required string Text { get; init; }

        //Options
        public required List<Option> Options { get; init; }
        public required Guid CorrectOption {  get; init; }
    }
}
