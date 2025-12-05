using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuizAppWpf
{
    public partial class CategoryWindow : Window
    {
        private readonly bool _isFrench;
        private readonly string _username;
        private readonly List<string> _programmingLanguages = new List<string> { "CSharp", "Java", "C", "Python", "SQL" };

        public CategoryWindow(bool isFrench, string username)
        {
            InitializeComponent();
            _isFrench = isFrench;
            _username = username;

            if (_isFrench)
            {
                Title = "Choisir une Catégorie";
                TitleText.Content = "Choisissez une catégorie";
            }

            LoadCategories();
        }

        private void LoadCategories()
        {
            string dataDir = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
            {
                string message = _isFrench 
                    ? "Aucune catégorie trouvée." 
                    : "No quiz categories found.";
                MessageBox.Show(message, _isFrench ? "Erreur" : "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            // Get all categories except programming languages
            var allFiles = Directory.EnumerateFiles(dataDir, "*.json")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var categories = allFiles
                .Where(cat => !_programmingLanguages.Contains(cat, StringComparer.OrdinalIgnoreCase))
                .OrderBy(x => x)
                .ToList();

            // Add "Programming Language" category
            string programmingCategory = _isFrench ? "Langage de programmation" : "Programming Language";
            categories.Add(programmingCategory);
            categories = categories.OrderBy(x => x).ToList();

            if (categories.Count == 0)
            {
                string message = _isFrench 
                    ? "Aucune catégorie trouvée." 
                    : "No quiz categories found.";
                MessageBox.Show(message, _isFrench ? "Erreur" : "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            CategoriesList.ItemsSource = categories;
        }

        private void CategoriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesList.SelectedItem is string category)
            {
                string programmingCategory = _isFrench ? "Langage de programmation" : "Programming Language";
                
                if (category.Equals(programmingCategory, StringComparison.OrdinalIgnoreCase))
                {
                    // Show programming language selection
                    ShowProgrammingLanguages();
                }
                else
                {
                    // Direct category selection
                    StartQuiz(category);
                }
            }
        }

        private void ShowProgrammingLanguages()
        {
            if (_isFrench)
            {
                TitleText.Content = "Choisissez un langage de programmation";
            }
            else
            {
                TitleText.Content = "Choose a Programming Language";
            }

            var languageNames = new List<string>();
            foreach (var lang in _programmingLanguages)
            {
                string displayName = lang switch
                {
                    "CSharp" => "C#",
                    "Java" => "Java",
                    "C" => "C",
                    "Python" => "Python",
                    "SQL" => "SQL",
                    _ => lang
                };
                languageNames.Add(displayName);
            }

            CategoriesList.ItemsSource = languageNames;
            CategoriesList.SelectionChanged -= CategoriesList_SelectionChanged;
            CategoriesList.SelectionChanged += ProgrammingLanguage_SelectionChanged;
        }

        private void ProgrammingLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesList.SelectedItem is string selectedLanguage)
            {
                // Map display name back to file name
                string fileName = selectedLanguage switch
                {
                    "C#" => "CSharp",
                    "Java" => "Java",
                    "C" => "C",
                    "Python" => "Python",
                    "SQL" => "SQL",
                    _ => selectedLanguage
                };

                // Pass both file name (for loading) and display name (for scores)
                StartQuiz(fileName, selectedLanguage);
            }
        }

        private void StartQuiz(string category, string? displayName = null)
        {
            // Use display name if provided, otherwise use category name
            string categoryForDisplay = displayName ?? category;
            var quizWindow = new QuizWindow(_isFrench, _username, category, categoryForDisplay);
            quizWindow.Show();
            this.Close();
        }
    }
}
