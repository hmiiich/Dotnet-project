using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace QuizAppWpf
{
    public partial class ScoresWindow : Window
    {
        private readonly bool _isFrench;
        private ObservableCollection<ScoreRecord> _scores;

        public ScoresWindow(bool isFrench)
        {
            InitializeComponent();
            _isFrench = isFrench;
            _scores = new ObservableCollection<ScoreRecord>();

            if (_isFrench)
            {
                Title = "Scores";
                TitleText.Content = "Scores du Quiz";
                RefreshButton.Content = "Actualiser";
                CloseButton.Content = "Fermer";
            }

            LoadScores();
        }

        private void LoadScores()
        {
            _scores.Clear();

            try
            {
                string scoresFile = Path.Combine(AppContext.BaseDirectory, "scores.txt");
                
                if (!File.Exists(scoresFile))
                {
                    if (_isFrench)
                    {
                        MessageBox.Show("Aucun score enregistré.", "Information", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No scores recorded yet.", "Information", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    return;
                }

                var lines = File.ReadAllLines(scoresFile);
                
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split('\t');
                    if (parts.Length >= 4)
                    {
                        _scores.Add(new ScoreRecord
                        {
                            Date = parts[0],
                            Username = parts[1],
                            Category = parts[2],
                            Score = parts[3]
                        });
                    }
                }

                // Sort by date (newest first)
                var sortedScores = _scores.OrderByDescending(s => s.Date).ToList();
                _scores.Clear();
                foreach (var score in sortedScores)
                {
                    _scores.Add(score);
                }

                ScoresDataGrid.ItemsSource = _scores;
            }
            catch (Exception ex)
            {
                string message = _isFrench 
                    ? $"Erreur lors du chargement des scores : {ex.Message}" 
                    : $"Error loading scores: {ex.Message}";
                MessageBox.Show(message, _isFrench ? "Erreur" : "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadScores();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class ScoreRecord
    {
        public string Date { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Score { get; set; } = string.Empty;
    }
}

