using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopApp.Services;

namespace DesktopApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string fullName = string.Empty;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        // set from code-behind because PasswordBox can't be data-bound safely
        public string Password { get; set; } = string.Empty;

        public event System.Action? RegisteredSuccessfully;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            ErrorMessage = string.Empty;

            var result = await _authService.RegisterAsync(FullName, UserName, Email, Password);

            if (!result.Succeeded)
            {
                ErrorMessage = string.Join("\n", result.Errors);
                return;
            }

            RegisteredSuccessfully?.Invoke();
        }
    }
}