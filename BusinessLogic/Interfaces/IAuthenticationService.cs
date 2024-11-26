namespace AuctionManagerApp.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        bool Login(string username, string email, out string role);
    }
}
