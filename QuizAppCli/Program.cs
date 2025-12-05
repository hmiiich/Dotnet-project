using QuizApp.Models;
using QuizApp.Services;

class Program
{
    static async Task Main(string[] args)
    {
        // Language option
        Console.WriteLine("Choose your language / Choisissez votre langue:");
        Console.WriteLine("1. English");
        Console.WriteLine("2. Français");
        int langChoice = 0;
        while (langChoice != 1 && langChoice != 2)
        {
            Console.Write("Enter number / Entrez le numéro: ");
            int.TryParse(Console.ReadLine(), out langChoice);
        }
        bool isFrench = langChoice == 2;
        
        string t(string en, string fr) => isFrench ? fr : en;
        // Username
        Console.Write(t("Please enter your username: ", "Veuillez entrer votre nom d'utilisateur: "));
        string username = Console.ReadLine()!.Trim();
        while (string.IsNullOrWhiteSpace(username))
        {
            Console.Write(t("Username cannot be empty. Please enter your username: ", "Le nom ne peut pas être vide. Veuillez entrer votre nom: "));
            username = Console.ReadLine()!.Trim();
        }

        // Clean category listing (by unique basename)
        string dataDir = Path.Combine(AppContext.BaseDirectory, "Data");
        var allFiles = Directory.EnumerateFiles(dataDir, "*.json");
        var uniqueCats = allFiles
            .Select(f => Path.GetFileNameWithoutExtension(f))
            .Select(name => name.TrimEnd(' ', '0','1','2','3','4','5','6','7','8','9').Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        if (uniqueCats.Count == 0)
        {
            Console.WriteLine(t("No quiz categories found. Exiting.", "Aucune catégorie trouvée. Sortie."));
            return;
        }
        bool keepPlaying = true;
        while (keepPlaying)
        {
            Console.WriteLine(t("\nChoose a category:", "\nChoisissez une catégorie:"));
            for (int i = 0; i < uniqueCats.Count; i++)
                Console.WriteLine($"  {i + 1}. {uniqueCats[i]}");
            int selected = -1;
            while (selected < 1 || selected > uniqueCats.Count)
            {
                Console.Write(t("Enter number: ", "Entrez le numéro: "));
                int.TryParse(Console.ReadLine(), out selected);
            }
            string chosenCategory = uniqueCats[selected - 1];
            // Always load from 'basename.json' (e.g. Science.json)
            string quizFile = Path.Combine(dataDir, chosenCategory + ".json");
            if (!File.Exists(quizFile))
            {
                Console.WriteLine(t($"No questions found for {chosenCategory}.", $"Aucune question trouvée pour {chosenCategory}."));
                continue;
            }
            var questions = await File.ReadAllTextAsync(quizFile);
            var doc = System.Text.Json.JsonDocument.Parse(questions);
            if (!doc.RootElement.TryGetProperty("questions", out var qArray))
            {
                Console.WriteLine(t($"No questions found for {chosenCategory}.", $"Aucune question trouvée pour {chosenCategory}."));
                continue;
            }
            var parsedQuestions = new List<QuizApp.Models.QuizQuestion>();
            foreach(var q in qArray.EnumerateArray())
            {
                parsedQuestions.Add(new QuizApp.Models.QuizQuestion{
                    Question = q.GetProperty("question").GetString() ?? "",
                    Answers = q.GetProperty("answers").EnumerateArray().Select(a => a.GetString() ?? "").ToList(),
                    CorrectIndex = q.GetProperty("correctIndex").GetInt32()
                });
            }
            int score = 0;
            for (int qidx = 0; qidx < parsedQuestions.Count; qidx++)
            {
                var q = parsedQuestions[qidx];
                Console.WriteLine();
                Console.WriteLine(t($"Question {qidx + 1}/{parsedQuestions.Count}:", $"Question {qidx + 1}/{parsedQuestions.Count} :"));
                Console.WriteLine(q.Question);
                for (int a = 0; a < q.Answers.Count; a++)
                    Console.WriteLine($"  {a + 1}. {q.Answers[a]}");
                int ans = -1;
                while (ans < 1 || ans > q.Answers.Count)
                {
                    Console.Write(t("Your answer: ", "Votre réponse: "));
                    int.TryParse(Console.ReadLine(), out ans);
                }
                if (ans - 1 == q.CorrectIndex)
                {
                    Console.WriteLine(isFrench ? "✅ Correct!" : "✅ Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine(t($"❌ Incorrect. Correct answer: {q.Answers[q.CorrectIndex]}", $"❌ Incorrect. La bonne réponse : {q.Answers[q.CorrectIndex]}") );
                }
            }
            Console.WriteLine("\n" + t("Quiz complete!", "Quiz terminé !"));
            Console.WriteLine(t($"{username}, your score: {score}/{parsedQuestions.Count}", $"{username}, votre score : {score}/{parsedQuestions.Count}"));
            try
            {
                string scoreLine = $"{DateTime.Now:u}\t{username}\t{chosenCategory}\t{score}/{parsedQuestions.Count}";
                System.IO.File.AppendAllLines("scores.txt", new[] { scoreLine });
                Console.WriteLine(t("Your score has been saved to scores.txt.", "Votre score a été enregistré dans scores.txt."));
            }
            catch
            {
                Console.WriteLine(t("Could not write scores.txt (permission issue?).\n", "Impossible d'écrire scores.txt (problème de permission ?)\n"));
            }
            Console.Write(t("\nWould you like to play another quiz? (Y/N): ", "Voulez-vous jouer une autre catégorie ? (O/N): "));
            var again = Console.ReadLine()?.Trim().ToUpper();
            if (!(again == (isFrench ? "O" : "Y")))
                keepPlaying = false;
        }
        Console.WriteLine(t("Thank you for playing QuizApp!", "Merci d'avoir joué à QuizApp !"));
    }
}
