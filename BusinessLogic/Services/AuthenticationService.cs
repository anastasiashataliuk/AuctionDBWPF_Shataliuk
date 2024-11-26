using AuctionManagerApp.BLL.Interfaces;

namespace AuctionManagerApp.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        // Simulated user data
        private readonly Dictionary<string, (string Email, string Role)> _users = new()
        {
            { "dylan", ("email@gmail.com", "Admin") },
            { "sam", ("samlee@gmail.com", "User") }
        };

        public bool Login(string username, string email, out string role)
        {
            // Replace with real authentication logic
            if (username == "12" && email == "123")
            {
                role = "Administrator";
                return true;
            }

            role = string.Empty;
            return false;
        }
    }
}
