using System;
using System.Windows.Input;

namespace AutoHideControl.Core
{
    public class DelegateCommand : ICommand
    {
        public bool CanExecute(object parameter) => CanExecuteAction?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => ExecuteAction?.Invoke(parameter);

        public Action<object> ExecuteAction { get; set; }

        public Func<object, bool> CanExecuteAction { get; set; }

        public event EventHandler CanExecuteChanged;
    }
}
