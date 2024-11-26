using DTO;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
        User AuthenticateUser(string username, string email);
        void RegisterUser(User user);
        List<User> GetAllUsers();
        void SaveUser(User user);
    }
}
