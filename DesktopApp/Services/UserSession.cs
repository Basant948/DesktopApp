using DesktopApp.Models;

namespace DesktopApp.Services
{
    public class UserSession
    {
        public ApplicationUser? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser is not null;

        public void SignIn(ApplicationUser user)
        {
            CurrentUser = user;
        }

        public void SignOut()
        {
            CurrentUser = null;
        }
    }
}
