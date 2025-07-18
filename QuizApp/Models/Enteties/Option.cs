namespace QuizApp.Models.Enteties
{
    public class Option
    {
        public Guid Id { get; init; }
        public required string Text { get; init; }

        //Relationship
        

        public required Guid QuestionId { get; init; }
    }
}
