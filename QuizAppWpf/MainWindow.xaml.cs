using System.Windows;
using System.Windows.Input;

namespace QuizAppWpf
{
    public partial class MainWindow : Window
    {
        public bool IsFrench { get; private set; }
        public string Username { get; private set; } = string.Empty;
        private bool _isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            _isInitialized = true;
        }

        private void UpdateLanguage()
        {
            if (FrenchRadio == null || UsernameLabel == null || StartButton == null || ViewScoresButton == null)
                return;
                
            IsFrench = FrenchRadio.IsChecked == true;
            if (IsFrench)
            {
                Title = "QuizApp - Bienvenue";
                UsernameLabel.Content = "Veuillez entrer votre nom d'utilisateur:";
                StartButton.Content = "Commencer le Quiz";
                ViewScoresButton.Content = "Voir les Scores";
            }
            else
            {
                Title = "QuizApp - Welcome";
                UsernameLabel.Content = "Please enter your username:";
                StartButton.Content = "Start Quiz";
                ViewScoresButton.Content = "View Scores";
            }
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Enter your name here..." || 
                (IsFrench && UsernameTextBox.Text == "Entrez votre nom ici..."))
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (FrenchRadio != null)
                IsFrench = FrenchRadio.IsChecked == true;
            Username = UsernameTextBox?.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(Username))
            {
                string message = IsFrench 
                    ? "Le nom ne peut pas être vide. Veuillez entrer votre nom." 
                    : "Username cannot be empty. Please enter your username.";
                MessageBox.Show(message, IsFrench ? "Erreur" : "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var categoryWindow = new CategoryWindow(IsFrench, Username);
            categoryWindow.Show();
            this.Close();
        }

        private void EnglishRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (_isInitialized && FrenchRadio != null && UsernameLabel != null && StartButton != null)
                UpdateLanguage();
        }

        private void FrenchRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (_isInitialized && FrenchRadio != null && UsernameLabel != null && StartButton != null)
                UpdateLanguage();
        }

        private void ViewScoresButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateLanguage();
            var scoresWindow = new ScoresWindow(IsFrench);
            scoresWindow.ShowDialog();
        }
    }
}

