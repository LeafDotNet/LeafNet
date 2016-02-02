using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace Leafnet.Wpf.Tests
{
  public partial class MainWindow
  {
    public const string Address = "Web//index.html";

    public MainWindow()
    {
      InitializeComponent();
      _browser.Address = Path.GetFullPath( "Web//index.html" );
      _browser.FrameLoadEnd += delegate
      {
        Dispatcher.Invoke( () =>
        {
          var viewmodel = new UnitTestsViewModel( _browser, Dispatcher );
          DataContext = viewmodel;
          viewmodel.RunUnitTests();
        } );
      };

      Closing += delegate
      {
        _browser.Dispose();
      };
    }
  }
}
