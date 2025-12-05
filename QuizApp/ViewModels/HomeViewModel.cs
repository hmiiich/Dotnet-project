using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizApp.Services;

namespace QuizApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<string> categories = new();

        private readonly QuizService quizService;

        public HomeViewModel()
        {
            quizService = new QuizService();
            Categories = quizService.GetCategories().ToList();
        }

        [RelayCommand]
        private async Task SelectCategory(string category)
        {
            // TODO: Navigate to QuizPage with category
        }
    }
}
