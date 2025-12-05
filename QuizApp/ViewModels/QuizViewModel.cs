using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizApp.Models;
using QuizApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuizApp.ViewModels
{
    public partial class QuizViewModel : ObservableObject
    {
        [ObservableProperty]
        private string questionNumberText = "Question 1/10";
        [ObservableProperty]
        private double progress = 0.1;
        [ObservableProperty]
        private string questionText = "What is the sample question?";
        [ObservableProperty]
        private string answer1 = "Answer 1";
        [ObservableProperty]
        private string answer2 = "Answer 2";
        [ObservableProperty]
        private string answer3 = "Answer 3";
        [ObservableProperty]
        private string answer4 = "Answer 4";
        [ObservableProperty]
        private string feedbackText = string.Empty;
        [ObservableProperty]
        private string feedbackColor = "#33CC33";
        [ObservableProperty]
        private bool isFeedbackVisible = false;
        [ObservableProperty]
        private bool isNextVisible = false;

        private List<QuizQuestion> questions = new();
        private int currentIndex = 0;
        private int score = 0;
        private string selectedCategory = string.Empty;
        private readonly QuizService quizService = new();

        public ICommand AnswerCommand { get; }
        public ICommand NextCommand { get; }

        public QuizViewModel()
        {
            AnswerCommand = new RelayCommand<int>(OnAnswerSelected);
            NextCommand = new RelayCommand(OnNext);
        }

        public async Task LoadCategory(string category)
        {
            // TODO: Implement load logic
        }
        private void OnAnswerSelected(int idx)
        {
            // TODO: Implement answer logic
        }
        private void OnNext()
        {
            // TODO: Implement next logic
        }
    }
}
