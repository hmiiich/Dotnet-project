# QuizApp

A modern cross-platform quiz app built with .NET 8 and .NET MAUI using MVVM. Supports macOS (Mac Catalyst), Windows, iOS, and Android.

## Features
- Choose from 6 categories: Geography, History, Sports, Science, Literature, Technology
- Each quiz has 10 questions with 4 multiple-choice answers
- Modern, clean UI with MAUI styling and feedback animations
- Progress bar and live feedback
- Easily extensible question set (edit/add JSON files in `Data/` folder)
- MVVM architecture (CommunityToolkit.Mvvm)

## Folder Structure
```
QuizApp/
├── Models/         # QuizQuestion, QuizCategory
├── Services/       # QuizService (loads JSON)
├── ViewModels/     # HomeViewModel, QuizViewModel, ResultViewModel
├── Views/          # HomePage, QuizPage, ResultPage (XAML + .cs)
├── Data/           # JSON files for each quiz category
├── Resources/      # Fonts, images, Styles
├── AppShell.xaml   # .NET MAUI Shell navigation
... etc
```

## Getting Started (macOS)
### Prerequisites
- [Install .NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Install Visual Studio 2022 for Mac](https://visualstudio.microsoft.com/vs/mac/) **with .NET MAUI workload**  
  OR use CLI to build/run.

### Run the App
1. Open a terminal and navigate to the project root (where QuizApp.csproj is).
2. Run:
   ```sh
   dotnet build -t:Run -f net9.0-maccatalyst
   ```
   Or open the solution in Visual Studio and run for Mac Catalyst target.

## Customizing Questions
- Each quiz category has a JSON file in the `/Data/` directory.
- Format example:
```json
{
  "category": "Geography",
  "questions": [
    {
      "question": "What is the capital of Japan?",
      "answers": ["Tokyo", "Osaka", "Kyoto", "Nara"],
      "correctIndex": 0
    }
  ]
}
```
- Add more or edit as needed!

## Project Credits
- Built using .NET 8, .NET MAUI, and CommunityToolkit.Mvvm
- Designed for macOS compatibility

## License
MIT
