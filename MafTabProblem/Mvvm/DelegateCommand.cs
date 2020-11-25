using System;
using System.Windows.Input;

namespace MafTabProblem.Mvvm
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}