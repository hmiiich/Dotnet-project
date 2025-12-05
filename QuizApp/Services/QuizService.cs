using System.Text.Json;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class QuizService
    {
        private readonly string dataFolder = Path.Combine(FileSystem.AppDataDirectory, "..", "Data");

        public async Task<List<QuizQuestion>> LoadQuestionsAsync(string category)
        {
            try
            {
                string filename = Path.Combine(AppContext.BaseDirectory, "Data", $"{category}.json");
                if (!File.Exists(filename))
                    throw new FileNotFoundException($"No data for {category}");

                string json = await File.ReadAllTextAsync(filename);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (!root.TryGetProperty("questions", out var questions))
                    return new List<QuizQuestion>();

                var list = new List<QuizQuestion>();
                foreach (var q in questions.EnumerateArray())
                {
                    var question = new QuizQuestion
                    {
                        Question = q.GetProperty("question").GetString() ?? "",
                        Answers = q.GetProperty("answers").EnumerateArray().Select(a => a.GetString() ?? "").ToList(),
                        CorrectIndex = q.GetProperty("correctIndex").GetInt32()
                    };
                    list.Add(question);
                }
                return list;
            }
            catch (Exception)
            {
                return new List<QuizQuestion>();
            }
        }

        public IEnumerable<string> GetCategories()
        {
            string basePath = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(basePath)) return Enumerable.Empty<string>();
            return Directory.EnumerateFiles(basePath, "*.json")
                .Select(f => Path.GetFileNameWithoutExtension(f));
        }
    }
}
