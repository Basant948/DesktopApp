using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DesktopApp.ViewModels;

namespace DesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.LoggedIn += OnLoggedIn;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
            await _viewModel.LoginCommand.ExecuteAsync(null);
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = App.AppHost.Services.GetRequiredService<RegisterWindow>();
            registerWindow.Show();
            Close();
        }

        private void OnLoggedIn()
        {
            // MainWindow lives in the root DesktopApp namespace, not DesktopApp.Views
            var mainWindow = App.AppHost.Services.GetRequiredService<DesktopApp.MainWindow>();
            mainWindow.Show();
            Close();
        }
    }
}
