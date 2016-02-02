using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Leafnet.Wpf.Tests.Util
{
  public class ViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null )
    {
      if ( EqualityComparer<T>.Default.Equals( field, value ) )
        return false;

      field = value;
      OnPropertyChanged( propertyName );

      return true;
    }

    protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
    {
      PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }
  }
}