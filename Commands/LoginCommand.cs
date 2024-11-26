using System;
using System.Windows.Input;

namespace AuctionManagerApp.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Constructor to initialize the command with execute and canExecute logic
        public LoginCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        // Checks whether the command can be executed
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        // Executes the command
        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
