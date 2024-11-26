using System.Windows;
using AuctionManagerApp.ViewModels;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;

namespace AuctionManagerApp.Views
{
    public partial class AuctionDetailsWindow : Window
    {
        private readonly IAuctionService _auctionService;
        private readonly int _auctionId;

        public AuctionDetailsWindow(IAuctionService auctionService, int auctionId, AuctionDetailsViewModel auctionDetailsView)
        {
            _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService)); // Null check
            _auctionId = auctionId;

            DataContext = auctionDetailsView;

            InitializeComponent();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Close the AuctionDetailsWindow
        }
    }
}
