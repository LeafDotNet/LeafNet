using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using Leafnet.Wpf.Tests.Util;

namespace Leafnet.Wpf.Tests
{
  public class UnitTestsViewModel : ViewModel
  {
    private readonly IWebBrowser _browser;
    private readonly Dispatcher _dispatcher;

    public UnitTestsViewModel(IWebBrowser browser, Dispatcher dispatcher)
    {
      _browser = browser;
      _dispatcher = dispatcher;
      Run = new DelegateCommand( RunUnitTests );
      ShowDevMode = new DelegateCommand( browser.ShowDevTools );
      UnitTests = new ObservableCollection<UnitTest>();
      Add( "2+2", "4" );
      Add( "L.version", "0.7.7" );
      Add( "map = L.map('map')", null );
      Add( "map.options.dragging", "True" );
    }

    public async void RunUnitTests()
    {
      foreach ( var unitTest in UnitTests )
      {
        var script = _browser.EvaluateScriptAsync( unitTest.JavaScript );
        await script;
        _dispatcher.Invoke( () =>
        {
          if ( !script.Result.Success )
            unitTest.Passed = PassState.Failed;
          else
          {
            unitTest.ActualResult = script.Result?.Result?.ToString();
            unitTest.Passed = unitTest.ActualResult == unitTest.ExpectedResult ? PassState.Passed :  PassState.Failed;
          }
        } );
      }
    }

    public ICommand ShowDevMode { get; set; }

    public ICommand Run { get; private set; }

    private void Add(string javascript, string expectedResult)
    {
     UnitTests.Add(new UnitTest( javascript, expectedResult ));
    }

    public ObservableCollection<UnitTest> UnitTests { get; }
  }
}