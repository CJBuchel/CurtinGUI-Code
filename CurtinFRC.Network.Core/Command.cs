using System;
using System.Windows.Input;

namespace DotNetDash
{
    /// <summary>
    /// Basic implementation of WPF command taking a typed parameter
    /// </summary>
    public class Command<T> : ICommand
    {
        public Command()
        { }

        public Command(Action<T> execute)
        {
            this.execute = execute;
        }

        public Command(Action<T> execute, Func<T, bool> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute;
        }

        private Func<T, bool> canExecute = val => true;
        private Action<T> execute;

        public bool CanExecute(object parameter) => canExecute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute?.Invoke((T)parameter);
        }
    }


    /// <summary>
    /// Basic implementation of WPF command taking no parameter
    /// </summary>
    public class Command : ICommand
    {
        public Command()
        { }

        public Command(Action execute)
        {
            this.execute = execute;
        }

        public Command(Action execute, Func<bool> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute;
        }

        private Func<bool> canExecute = () => true;
        private Action execute;

        public bool CanExecute(object ignored) => canExecute();

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object ignored)
        {
            execute?.Invoke();
        }
    }
}
