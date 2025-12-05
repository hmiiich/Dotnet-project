using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace QuizApp.ViewModels
{
    public partial class ResultViewModel : ObservableObject
    {
        [ObservableProperty]
        private string finalScoreText = "0/10";

        public ResultViewModel() { }

        [RelayCommand]
        void Restart()
        {
            // TODO: Add navigation restart logic
        }
    }
}
