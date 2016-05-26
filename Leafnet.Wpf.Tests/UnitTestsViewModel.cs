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
      Add( "map.setView([51.505, -0.09], 13);", null );
      Add( "map.getZoom();", "13" );
      Add( "map.getBounds().contains([51.505, -0.09]);", "True" );
      Add( "tileLayer = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {\"maxZoom\":10})", null );
      Add( "tileLayer.options.maxZoom", "10");
      Add( "tileLayer.addTo(map);", null );
      Add( "tileLayer._url", null );
    }

    public async void RunUnitTests()
    {
      foreach ( var unitTest in UnitTests )
      {
        var script = await _browser.EvaluateScriptAsync( unitTest.JavaScript );
        _dispatcher.Invoke( () =>
        {
          if ( !script.Success )
            unitTest.Passed = PassState.Failed;
          else
          {
            unitTest.ActualResult = script.Result?.ToString();
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