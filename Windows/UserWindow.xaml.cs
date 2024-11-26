using AuctionManagerApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AuctionManagerApp.Views
{
    public partial class UserWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public UserWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void GoToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Resolve MainWindow from the DI container
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();  // Show the main window

            this.Close();  // Close the current UserWindow
        }


    }

}

