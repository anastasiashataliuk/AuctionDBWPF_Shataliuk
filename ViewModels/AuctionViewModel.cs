using AuctionManagerApp.Commands;
using System.Windows;
using System.Windows.Input;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;
using AuctionManagerApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionManagerApp.ViewModels
{
    public class AuctionViewModel : BaseViewModel
    {
        private readonly IAuctionService _auctionService;
        private readonly IServiceProvider _serviceProvider; // Add IServiceProvider

        public ICommand CreateAuctionCommand { get; }
        public ICommand ViewAuctionsCommand { get; }
        public ICommand BackToDashboardCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand ViewAuctionDetailsCommand { get; }

        public AuctionViewModel(IAuctionService auctionService, IServiceProvider serviceProvider)
        {

            _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService));
            _serviceProvider = serviceProvider;

            CreateAuctionCommand = new RelayCommand(CreateAuction);  // CreateAuction should be a valid Action<object>
            ViewAuctionsCommand = new RelayCommand(ViewAuctions);
            BackToDashboardCommand = new RelayCommand(BackToDashboard);
            AddProductCommand = new RelayCommand(AddProductToAuction);
            ViewAuctionDetailsCommand = new RelayCommand(ViewAuctionDetails);
        }

        private void CreateAuction(object parameter)
        {
            var auctionDetailsWindow = _serviceProvider.GetRequiredService<AuctionDetailsWindow>(); // Use DI to get the window
            auctionDetailsWindow.ShowDialog();
        }

        private void ViewAuctions(object parameter)
        {
            MessageBox.Show("View Auctions clicked!");
        }

        private void BackToDashboard(object parameter)
        {
            MessageBox.Show("Back to User Dashboard clicked!");
        }

        private void AddProductToAuction(object parameter)
        {
            MessageBox.Show("Add Product to Auction clicked!");
        }

        private void ViewAuctionDetails(object parameter)
        {
            if (parameter is int auctionId)
            {
                var auctionDetailsWindow = _serviceProvider.GetRequiredService<AuctionDetailsWindow>(); // Use DI to get the window
                auctionDetailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid auction selected.");
            }
        }
    }

}
