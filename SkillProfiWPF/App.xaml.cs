using SkillProfi;
using SkillProfiWPF.Models;
using SkillProfiWPF.ViewModels;
using SkillProfiWPF.Views;
using System.Diagnostics;
using System.Net.Mail;
using System.Windows;

namespace SkillProfiWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var dialog = new LoginWindow();

            if (dialog.ShowDialog() == true)
            {
                AuthParameters ap =  (dialog.DataContext as LoginViewModel).AuthParams;

                AuthData.Login = ap.Login;
                AuthData.AccessToken = ap.AccessToken;
                AuthData.IsLogin = ap.IsLogin;

                var mainWindow = new MainWindow();
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();
 

            }

        }
    }
}
