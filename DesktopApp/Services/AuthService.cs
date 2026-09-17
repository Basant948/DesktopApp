using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DesktopApp.Models;

namespace DesktopApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserSession _session;

        public AuthService(UserManager<ApplicationUser> userManager, UserSession session)
        {
            _userManager = userManager;
            _session = session;
        }

        public async Task<AuthResult> RegisterAsync(string fullName, string userName, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FullName = fullName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToArray()
                };
            }

            return new AuthResult { Succeeded = true };
        }

        public async Task<AuthResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return new AuthResult { Succeeded = false, Errors = new[] { "Invalid username or password." } };
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordOk)
            {
                return new AuthResult { Succeeded = false, Errors = new[] { "Invalid username or password." } };
            }

            _session.SignIn(user);
            return new AuthResult { Succeeded = true };
        }
    }
}