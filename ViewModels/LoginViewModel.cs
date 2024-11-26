using AuctionManagerApp.BLL.Interfaces;
using AuctionManagerApp.Commands;
using System.ComponentModel;
using System.Windows;
using System;

namespace AuctionManagerApp.ViewModels
{
    public class LoginViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Action _onLoginSuccess;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public RelayCommand LoginCommand { get; }

        // Constructor receives MainWindow and an Action callback
        public LoginViewModel(IAuthenticationService authenticationService, Action navigateToMainWindow)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _onLoginSuccess = navigateToMainWindow ?? throw new ArgumentNullException(nameof(navigateToMainWindow));

            LoginCommand = new RelayCommand(Login);
        }


        private void Login(object parameter)
        {
            if (_authenticationService.Login(Username, Email, out var role))
            {
                ErrorMessage = "Login Successful";
                MessageBox.Show($"Logged in as {role}");

                // Call the action to open the MainWindow
                _onLoginSuccess.Invoke();
            }
            else
            {
                ErrorMessage = "Invalid username or email!";
            }
        }

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case nameof(Username):
                        if (string.IsNullOrWhiteSpace(Username))
                        {
                            error = "Username is required.";
                        }
                        break;

                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            error = "Email is required.";
                        }
                        else if (!Email.Contains("@"))
                        {
                            error = "Invalid email format.";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => null; // Not used for per-property validation
    }
}
