using System;
using System.Windows.Input;

namespace AuctionManagerApp.Commands
{
    public class CancelCommand : ICommand
    {
        private readonly Action _execute;

        public CancelCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            return true; // Always executable
        }

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
