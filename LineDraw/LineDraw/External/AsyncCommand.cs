/*
 * https://msdn.microsoft.com/en-us/magazine/dn630647.aspx 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.External
{
    public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged, IDisposable
    {
        private readonly Func<Task<TResult>> _command;
        private NotifyTaskCompletion<TResult> _execution;
        private Predicate<object> canExecute;

        public AsyncCommand(Func<Task<TResult>> command, Predicate<object> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            _command = command;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public override Task ExecuteAsync(object parameter)
        {
            Execution = new NotifyTaskCompletion<TResult>(_command());
            return Execution.TaskCompletion;
        }

        public NotifyTaskCompletion<TResult> Execution
        {
            get { return _execution; }
            private set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
           this.canExecute = _ => false;
           this._execution = null;
        }
    }
}
