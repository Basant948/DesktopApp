using System.Threading.Tasks;
using DesktopApp.Models;

namespace DesktopApp.Services
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = System.Array.Empty<string>();
    }

    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string fullName, string userName, string email, string password);
        Task<AuthResult> LoginAsync(string userName, string password);
    }
}