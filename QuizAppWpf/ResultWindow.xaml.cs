using System.Windows;

namespace QuizAppWpf
{
    public partial class ResultWindow : Window
    {
        private readonly bool _isFrench;
        private readonly string _username;
        private readonly int _score;
        private readonly int _total;
        private readonly string _category;

        public ResultWindow(bool isFrench, string username, int score, int total, string category)
        {
            InitializeComponent();
            _isFrench = isFrench;
            _username = username;
            _score = score;
            _total = total;
            _category = category;

            if (_isFrench)
            {
                Title = "Résultats";
                TitleText.Content = "Quiz terminé !";
                ScoreText.Content = $"{_username}, votre score : {_score}/{_total}";
                CategoryText.Content = $"Catégorie : {_category}";
                MessageText.Text = "Votre score a été enregistré dans scores.txt.";
                PlayAgainButton.Content = "Rejouer";
                ViewScoresButton.Content = "Voir les Scores";
                ExitButton.Content = "Quitter";
            }
            else
            {
                Title = "Results";
                TitleText.Content = "Quiz Complete!";
                ScoreText.Content = $"{_username}, your score: {_score}/{_total}";
                CategoryText.Content = $"Category: {_category}";
                MessageText.Text = "Your score has been saved to scores.txt.";
                PlayAgainButton.Content = "Play Again";
                ViewScoresButton.Content = "View Scores";
                ExitButton.Content = "Exit";
            }

            // Calculate percentage
            double percentage = (_score * 100.0) / _total;
            string message = MessageText.Text ?? "";
            if (percentage >= 80)
            {
                message += _isFrench 
                    ? "\nExcellent travail ! 🌟" 
                    : "\nExcellent work! 🌟";
            }
            else if (percentage >= 60)
            {
                message += _isFrench 
                    ? "\nBon travail ! 👍" 
                    : "\nGood work! 👍";
            }
            else
            {
                message += _isFrench 
                    ? "\nContinuez à vous améliorer ! 💪" 
                    : "\nKeep improving! 💪";
            }
            MessageText.Text = message;
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryWindow(_isFrench, _username);
            categoryWindow.Show();
            this.Close();
        }

        private void ViewScoresButton_Click(object sender, RoutedEventArgs e)
        {
            var scoresWindow = new ScoresWindow(_isFrench);
            scoresWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = _isFrench 
                ? "Merci d'avoir joué à QuizApp !" 
                : "Thank you for playing QuizApp!";
            MessageBox.Show(message, _isFrench ? "Au revoir" : "Goodbye", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Shutdown();
        }
    }
}

