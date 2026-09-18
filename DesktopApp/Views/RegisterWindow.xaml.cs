using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DesktopApp.ViewModels;

namespace DesktopApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly RegisterViewModel _viewModel;

        public RegisterWindow(RegisterViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.RegisteredSuccessfully += OnRegistered;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
            await _viewModel.RegisterCommand.ExecuteAsync(null);
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = App.AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }

        private void OnRegistered()
        {
            MessageBox.Show("Account created. Please log in.");
            var loginWindow = App.AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Close();
        }
    }
}
