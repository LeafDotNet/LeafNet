using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Leafnet.Wpf.Tests.Util
{
  internal class DelegateCommand : ICommand
  {
    private readonly Func<bool> _canExecuteHandler;
    private readonly Action _commandHandler;

    public DelegateCommand( Action commandHandler, Func<bool> canExecuteHandler = null )
    {
      _commandHandler = commandHandler;
      _canExecuteHandler = canExecuteHandler;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute( object parameter )
    {
      _commandHandler();
    }

    public bool CanExecute( object parameter )
    {
      return _canExecuteHandler == null || _canExecuteHandler();
    }

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke( this, new EventArgs() );
    }
  }
}