/*
 * Based upon online source found at
 * https://social.msdn.microsoft.com/Forums/vstudio/en-US/b59975f9-3039-42af-b6b6-9c6d17079d24/binding-mouse-position-in-mvvm-is-it-possible?forum=wpf.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LineDraw.External
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        private string _displayText;

        public static List<string> Log = new List<string>();

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            : this(execute, canExecute, "")
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute, string displayText)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            _displayText = displayText;
        }

        public string DisplayText
        {
            get { return _displayText; }
            set { _displayText = value; }
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {

            _execute(parameter);
        }

        #endregion // ICommand Members
    }    
}
