using DTO;

namespace BusinessLogic.Interfaces
{
    public interface IAuctionService
    {
        string Role { get; }

        List<Auction> GetAllAuctions();
        Auction GetAuctionById(int auctionId);
        void CreateAuction(Auction auction);
        void DeleteAuction(int auctionId);
        void SaveAuction(Auction auction);
        void UpdateAuction(Auction auction);
    }
}
