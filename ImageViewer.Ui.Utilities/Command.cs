using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageViewer.Ui.Utilities
{
    public class Command : ICommand
    {
        private bool _canExecute = true;
        private readonly Action<object> _parametrizedAction;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object> parametrizedAction)
        {
            _parametrizedAction = parametrizedAction;
            _canExecute = true;
        }

        public Command(Action<object> parametrizedAction, bool canExecute)
        {
            _parametrizedAction = parametrizedAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (_canExecute)
            {
                _parametrizedAction.Invoke(parameter);
            }
        }
    }
}
