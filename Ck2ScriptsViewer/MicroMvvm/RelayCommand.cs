﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Ck2ScriptsViewer.MicroMvvm
{
	/// <summary>
	/// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
	/// </summary>
	[Serializable]
	public class RelayCommand<T> : ICommand
	{
		#region Declarations

		private readonly Predicate<T> _canExecute;
		private readonly Action<T> _execute;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		public RelayCommand(Action<T> execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion

		#region ICommand Members

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		[DebuggerStepThrough]
		public Boolean CanExecute(Object parameter)
		{
			return _canExecute == null || _canExecute((T)parameter);
		}

		public void Execute(Object parameter)
		{
			_execute((T)parameter);
		}

		#endregion
	}

	/// <summary>
	/// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
	/// </summary>
	[Serializable]
	public class RelayCommand : ICommand
	{
		#region Declarations

		private readonly Func<Boolean> _canExecute;
		private readonly Action _execute;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		public RelayCommand(Action execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		public RelayCommand(Action execute, Func<Boolean> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion

		#region ICommand Members

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		[DebuggerStepThrough]
		public Boolean CanExecute(Object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		public void Execute(Object parameter)
		{
			_execute();
		}

		#endregion
	}
}
