using System;
using System.Threading.Tasks;

namespace DesktopApp.Services
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
    }

    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string fullName, string userName, string email, string password);
        Task<AuthResult> LoginAsync(string userName, string password);
    }
}
