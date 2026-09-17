using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopApp.Services;

namespace DesktopApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public string Password { get; set; } = string.Empty;

        public event System.Action? LoggedIn;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;

            var result = await _authService.LoginAsync(UserName, Password);

            if (!result.Succeeded)
            {
                ErrorMessage = string.Join("\n", result.Errors);
                return;
            }

            LoggedIn?.Invoke();
        }
    }
}