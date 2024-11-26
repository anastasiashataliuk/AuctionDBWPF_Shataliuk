using AuctionManagerApp.BLL.Interfaces;
using AuctionManagerApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AuctionManagerApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            // Set the DataContext to the injected ViewModel
            DataContext = loginViewModel;
        }
    }
}
