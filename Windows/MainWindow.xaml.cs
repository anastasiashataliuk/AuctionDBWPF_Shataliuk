using AuctionManagerApp.Views;
using AuctionManagerApp.BLL.Interfaces;
using System.Windows;
using BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionManagerApp
{
    public partial class MainWindow : Window
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserWindow _userWindow;
        private readonly LoginWindow _loginWindow;
        private readonly IAuctionService _auctionService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IAuthenticationService authenticationService, UserWindow userWindow, LoginWindow loginWindow, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authenticationService = authenticationService;
            _userWindow = userWindow;
            _loginWindow = loginWindow;
            _serviceProvider = serviceProvider;
        }

        private void GoToAuctionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Resolve AuctionWindow from DI and show it
            var auctionWindow = _serviceProvider.GetRequiredService<AuctionWindow>();
            auctionWindow.ShowDialog(); // or Show() depending on the behavior you want
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(this); // Pass MainWindow reference
            addProductWindow.Show();
            this.Hide(); // Hide instead of closing
        }

        private void OpenUserWindow_Click(object sender, RoutedEventArgs e)
        {
            _userWindow.Owner = this; // Set MainWindow as owner
            _userWindow.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _loginWindow.Show();
        }
    }
}
