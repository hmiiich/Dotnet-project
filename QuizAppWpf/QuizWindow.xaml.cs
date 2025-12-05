using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizAppWpf
{
    public partial class QuizWindow : Window
    {
        private readonly bool _isFrench;
        private readonly string _username;
        private readonly string _category;
        private readonly string _categoryDisplayName;
        private readonly List<QuizQuestion> _questions;
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        private bool _isAnswered = false;

        public QuizWindow(bool isFrench, string username, string category, string? categoryDisplayName = null)
        {
            InitializeComponent();
            _isFrench = isFrench;
            _username = username;
            _category = category;
            _categoryDisplayName = categoryDisplayName ?? category;

            if (_isFrench)
            {
                Title = "Quiz";
            }

            _questions = new List<QuizQuestion>();
            this.Loaded += QuizWindow_Loaded;
        }

        private async void QuizWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var service = new QuizService();
            _questions.AddRange(await service.LoadQuestionsAsync(_category));

            if (_questions.Count == 0)
            {
                string message = _isFrench 
                    ? $"Aucune question trouvée pour {_category}." 
                    : $"No questions found for {_category}.";
                MessageBox.Show(message, _isFrench ? "Erreur" : "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
            {
                ShowResults();
                return;
            }

            _isAnswered = false;
            var question = _questions[_currentQuestionIndex];

            ProgressText.Content = _isFrench 
                ? $"Question {_currentQuestionIndex + 1}/{_questions.Count}"
                : $"Question {_currentQuestionIndex + 1}/{_questions.Count}";

            QuestionText.Text = question.Question;
            
            // Clear previous answers
            AnswersPanel.Children.Clear();
            
            // Add answer buttons
            foreach (var answer in question.Answers)
            {
                var button = new Button
                {
                    Content = answer,
                    FontSize = 14,
                    Height = 40,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = answer
                };
                button.Click += AnswerButton_Click;
                AnswersPanel.Children.Add(button);
            }
            
            FeedbackText.Visibility = Visibility.Collapsed;
        }

        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAnswered) return;

            if (sender is Button button && button.Tag is string selectedAnswer)
            {
                _isAnswered = true;
                var question = _questions[_currentQuestionIndex];
                int selectedIndex = question.Answers.IndexOf(selectedAnswer);
                bool isCorrect = selectedIndex == question.CorrectIndex;

                // Disable all buttons and highlight correct/incorrect
                foreach (Button btn in AnswersPanel.Children.OfType<Button>())
                {
                    btn.IsEnabled = false;
                    string answerText = btn.Tag as string ?? "";
                    if (answerText == question.Answers[question.CorrectIndex])
                    {
                        btn.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                    else if (answerText == selectedAnswer && !isCorrect)
                    {
                        btn.Background = new SolidColorBrush(Colors.LightCoral);
                    }
                }

                if (isCorrect)
                {
                    _score++;
                    FeedbackText.Content = _isFrench ? "✅ Correct !" : "✅ Correct!";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    string correctAnswer = question.Answers[question.CorrectIndex];
                    FeedbackText.Content = _isFrench 
                        ? $"❌ Incorrect. La bonne réponse : {correctAnswer}" 
                        : $"❌ Incorrect. Correct answer: {correctAnswer}";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Red);
                }

                FeedbackText.Visibility = Visibility.Visible;

                // Wait 2 seconds before next question
                await Task.Delay(2000);
                _currentQuestionIndex++;
                LoadQuestion();
            }
        }

        private void ShowResults()
        {
            try
            {
                string scoreLine = $"{DateTime.Now:u}\t{_username}\t{_categoryDisplayName}\t{_score}/{_questions.Count}";
                string scoresFile = Path.Combine(AppContext.BaseDirectory, "scores.txt");
                
                // Ensure the file exists or create it
                if (!File.Exists(scoresFile))
                {
                    File.Create(scoresFile).Close();
                }
                
                File.AppendAllLines(scoresFile, new[] { scoreLine });
            }
            catch (Exception ex)
            {
                // Log error but don't block the application
                System.Diagnostics.Debug.WriteLine($"Error saving score: {ex.Message}");
            }

            var resultWindow = new ResultWindow(_isFrench, _username, _score, _questions.Count, _categoryDisplayName);
            resultWindow.Show();
            this.Close();
        }
    }
}
