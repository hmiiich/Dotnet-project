namespace QuizApp.Models
{
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new();
        public int CorrectIndex { get; set; }
    }
}
