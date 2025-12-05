using System;
using System.Windows;
using System.Windows.Threading;

namespace QuizAppWpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Gestion des exceptions non gérées
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"Une erreur s'est produite :\n{e.Exception.Message}\n\n{e.Exception.StackTrace}",
                "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"Une erreur critique s'est produite :\n{e.ExceptionObject}",
                "Erreur Critique",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}

