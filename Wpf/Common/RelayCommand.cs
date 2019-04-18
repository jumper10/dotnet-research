using System;
using System.Windows.Input;

namespace Wpf_research.Common
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Func<object, bool> canExecute;
        public Action<object> execute;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null) return true;

            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
