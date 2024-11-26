using System.Windows;
using AuctionManagerApp.ViewModels;
using AuctionManagerApp.Views;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;

namespace AuctionManagerApp
{
    public partial class AuctionWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly AuctionDetailsWindow _detailsWindow;
        private readonly IAuctionService _auctionService;

        public AuctionWindow(IAuctionService auctionService)
        {
            _auctionService = auctionService;
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the current AuctionWindow
            _mainWindow.Show(); // Show MainWindow
        }
    }
}
