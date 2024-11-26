using AuctionManagerApp.Commands;
using BusinessLogic.Interfaces;
using DALEF.Models; // Namespace for your DbContext
using DTO;
using System;
using System.Windows;
using System.Windows.Input;

namespace AuctionManagerApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly AuctionDBContext DBcontext;

        private readonly IUserService _userService;
        public UserViewModel(IUserService userService)
        {
            _userService = userService;
        }


        // Properties for user data
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Command logic for Save
        public void Save(object parameter)
        {
            if (_userService == null)  // Assuming _userService is causing the issue
            {
                MessageBox.Show("UserService is not initialized!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add other checks for any dependencies or fields being used
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Username is required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Proceed with the save logic
            try
            {
                _userService.SaveUser(new User { Name = Username });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Command logic for Cancel
        private void Cancel(object parameter)
        {
            // Logic to cancel the action, e.g., reset user fields
            Username = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            Console.WriteLine("Action canceled!");
        }

        // CanSave logic (if any conditions are required)
        private bool CanSave(object parameter)
        {
            // Example: Ensure the username and email are not empty
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email);
        }
    }
}
