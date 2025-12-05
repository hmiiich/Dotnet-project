# QuizApp WPF - Interface Graphique

Application de quiz avec interface graphique WPF pour Windows.

## Fonctionnalités

- ✅ Interface graphique moderne et intuitive
- ✅ Support bilingue (Français/English)
- ✅ Sélection de catégorie de quiz
- ✅ Affichage des questions avec réponses multiples
- ✅ Feedback visuel immédiat (correct/incorrect)
- ✅ Sauvegarde automatique des scores
- ✅ Affichage des résultats avec statistiques

## Structure

- **MainWindow** : Fenêtre d'accueil avec sélection de langue et nom d'utilisateur
- **CategoryWindow** : Sélection de la catégorie de quiz
- **QuizWindow** : Interface de quiz avec questions et réponses
- **ResultWindow** : Affichage des résultats et options pour rejouer

## Compilation et Exécution

```bash
cd QuizAppWpf
dotnet build
dotnet run
```

Ou ouvrez le projet dans Visual Studio et appuyez sur F5.

## Données

Les fichiers JSON de quiz doivent être placés dans le dossier `Data/` :
- Geography.json
- History.json
- Literature.json
- Science.json
- Sports.json
- Technology.json

Les scores sont sauvegardés dans `scores.txt` à la racine de l'application.


