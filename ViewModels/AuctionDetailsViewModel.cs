using BusinessLogic.Interfaces;
using System;
using System.Windows.Input;
using AuctionManagerApp.Commands;
using DTO;
using System.Diagnostics;

namespace AuctionManagerApp.ViewModels
{
    public class AuctionDetailsViewModel : BaseViewModel
    {
        private readonly IAuctionService _auctionService;
        private Auction _auction;

        // Properties to bind to the View
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal BuyoutPrice { get; set; }
        public string Status { get; set; }

        // Command to save the auction
        public ICommand SaveAuctionCommand { get; }

        // Constructor accepting IAuctionService and auctionId
        public AuctionDetailsViewModel(IAuctionService auctionService, int auctionId)
        {
            _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService));

            // Fallback to creating a new auction if auctionId is invalid or if no auction is found
            if (auctionId <= 0)
            {
                _auctionService = auctionService;
            }
            else
            {
                // Try to load the auction details
                LoadAuctionDetails(auctionId);

                // If no auction was found, initialize a new one
                if (_auction == null)
                {
                    _auction = new Auction();
                    // Optionally log that the auction was not found and a new one was created
                    Console.WriteLine($"Auction with ID {auctionId} not found, creating a new one.");
                }
            }

            SaveAuctionCommand = new RelayCommand(SaveAuction);
        }

        private void LoadAuctionDetails(int auctionId)
        {
            // Fetch auction from service by ID
            _auction = _auctionService.GetAuctionById(auctionId);

            // Check if auction is null and handle accordingly
            if (_auction == null)
            {
                // Log if auction is not found or handle appropriately
                Console.WriteLine($"Auction with ID {auctionId} not found.");
            }
        }



        private void SaveAuction(object parameter)
        {
            // Debugging: Log auction and service state
            Debug.WriteLine($"_auction: {_auction}");
            Debug.WriteLine($"_auctionService: {_auctionService}");

            if (_auction == null)
            {
                _auction = new Auction();
            }

            if (_auctionService == null)
            {
                throw new InvalidOperationException("Auction service is not initialized.");
            }

            // Continue with auction saving logic
            _auction.Start_Date = StartDate;
            _auction.End_Date = EndDate;
            _auction.Starting_Price = StartingPrice;
            _auction.Buyout_Price = BuyoutPrice;
            _auction.Status = Status;

            if (_auction.Auction_Id == 0)
            {
                _auctionService.CreateAuction(_auction);
            }
            else
            {
                _auctionService.UpdateAuction(_auction);
            }

            if (parameter is System.Windows.Window window)
            {
                window.Close();
            }
        }

    }
}
