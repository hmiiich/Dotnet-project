# QuizApp - .NET Project

Application de quiz multi-plateforme développée en .NET avec trois interfaces différentes.

## 📁 Structure du Projet

Le projet contient trois applications :

### 1. **QuizApp** - Application MAUI (Multi-platform)
Application multiplateforme utilisant .NET MAUI pour Android, iOS, Windows, macOS, etc.

### 2. **QuizAppCli** - Application Console
Application en ligne de commande pour Windows/Linux/macOS.

### 3. **QuizAppWpf** - Application WPF (Windows)
Application avec interface graphique Windows utilisant WPF.

## 🚀 Fonctionnalités

- ✅ Support bilingue (Français/English)
- ✅ Plusieurs catégories de quiz :
  - Geography
  - History
  - Literature
  - Science
  - Sports
  - Technology
  - **Programming Languages** (C#, Java, C, Python, SQL)
- ✅ Sauvegarde automatique des scores
- ✅ Interface graphique moderne (WPF)
- ✅ Affichage des scores avec historique

## 🛠️ Technologies Utilisées

- .NET 9.0
- WPF (Windows Presentation Foundation)
- .NET MAUI (Multi-platform App UI)
- System.Text.Json

## 📦 Installation

### Prérequis
- .NET 9.0 SDK ou supérieur
- Visual Studio 2022 ou VS Code (pour WPF)
- Visual Studio 2022 avec support MAUI (pour QuizApp)

### Compilation

#### QuizAppCli (Console)
```bash
cd QuizAppCli
dotnet build
dotnet run
```

#### QuizAppWpf (WPF)
```bash
cd QuizAppWpf
dotnet build
dotnet run
```

#### QuizApp (MAUI)
Ouvrez le projet dans Visual Studio 2022 et exécutez-le.

## 📝 Format des Scores

Les scores sont enregistrés dans `scores.txt` avec le format suivant :
```
Date    Username    Category    Score/Total
```

Exemple :
```
2024-01-15T10:30:00Z    John    C#    5/7
```

## 🎮 Utilisation

### Application WPF
1. Lancez l'application
2. Choisissez votre langue (Français/English)
3. Entrez votre nom d'utilisateur
4. Sélectionnez une catégorie
5. Pour les langages de programmation, choisissez le langage spécifique
6. Répondez aux questions
7. Consultez vos scores via le bouton "View Scores"

### Application CLI
1. Exécutez l'application
2. Suivez les instructions à l'écran

## 📂 Structure des Données

Les questions sont stockées dans des fichiers JSON dans le dossier `Data/` de chaque application.

Format JSON :
```json
{
  "category": "CategoryName",
  "questions": [
    {
      "question": "Question text?",
      "answers": ["Answer 1", "Answer 2", "Answer 3", "Answer 4"],
      "correctIndex": 0
    }
  ]
}
```

## 👤 Auteur

hmiiich

## 📄 Licence

Ce projet est fourni tel quel pour des fins éducatives.

