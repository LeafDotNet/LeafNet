﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CefSharp;
using CefSharp.Wpf;
using Leafnet.Wpf.Tests.MapTest.UnitTests;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest.FullTest
{
  public abstract class FullTestBase : ReactiveObject, IFullTest
  {
    private ChromiumWebBrowser _browser;
    private IDisposable _disposal = Disposable.Empty;

    protected FullTestBase()
    {
      Control = new UserControl();
      IObservable<bool> isLoaded = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
        addHandler: handler => Control.Loaded += handler,
        removeHandler: handler => Control.Loaded -= handler ).Select( e => Control.IsLoaded);
//      RunTest = ReactiveCommand.CreateAsyncTask( isLoaded, _ => Run(), RxApp.MainThreadScheduler);
      var command = ReactiveCommand.Create( isLoaded, RxApp.MainThreadScheduler );
      command.Subscribe( Run );
      RunTest = command;
    }

    private void Run(object o)
    {
      _browser?.Dispose();
      _disposal.Dispose();

      State = TestStates.Running;
      _browser = new ChromiumWebBrowser
      {
        Address = Path.GetFullPath( "Web//index.html" )
      };
      var browserLoaded = Observable.FromEventPattern<EventHandler<FrameLoadEndEventArgs>, FrameLoadEndEventArgs>(
        h => _browser.FrameLoadEnd += h, h => _browser.FrameLoadEnd -= h );
      Control.Content = _browser;

      _disposal = browserLoaded.Subscribe( async _ =>
      {
        await Setup( _browser );
        await Tests( _browser );
      } );
    }

    public abstract Task Setup(IWebBrowser browser);

    public abstract Task<bool> Tests(IWebBrowser browser);

    public void Dispose()
    {
      State = TestStates.NotRunning;
      RunTest.Dispose();
    }

    public UserControl Control { get; set; }
    public TestStates State { get; set; }
    public string Description { get; set; }
    public IReactiveCommand RunTest { get; set; }
    public bool IsDisposed { get; set; }
    public IReactiveList<string> DebugOutput { get; set; }
  }

  public class BasicMapTest : FullTestBase
  {
    private L _leaflet;

    public override Task Setup( IWebBrowser browser)
    {
      _leaflet = new L( browser );
      return Task.FromResult(true);
    }

    public override async Task<bool> Tests(IWebBrowser browser)
    {
      Map map = new Map( "map", browser, "map" );
      await _leaflet.Map( map );
      await _leaflet.Evaluate( "map.setView([51.505, -0.09], 13);" );

      var url = @"https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw";
      var tileOptions = new TileLayerOptions { id = "mapbox.streets" };
      var tileLayer = new TileLayer( "mapboxTileLayer", browser, url, tileOptions );
      await ( await _leaflet.TileLayer( tileLayer ) ).AddToMap( map );
      return true;
    }
  }


  /// <summary>
  /// A full test completely encapsulates the entirety of the test
  /// so that is can be independent of any preconditions
  /// </summary>
  public interface IFullTest : IReactiveObject, IDisposable
  {
    /// <summary>
    /// Control that will contain the browser.
    /// Not using the actual WpfWebBrowser so that we can clean it up in the test internally
    /// </summary>
    UserControl Control { get; }
    /// <summary>
    /// Current state of the test
    /// </summary>
    TestStates State { get; }
    /// <summary>
    /// Description of the Test
    /// </summary>
    string Description { get; }
    /// <summary>
    /// Command that will run the test
    /// This command wil call dispose if the test has already ran
    /// it will then create the browser and leafnet map
    /// then run the tests
    /// </summary>
    IReactiveCommand RunTest { get; }

    /// <summary>
    /// Is the test disposed of or not
    /// This will also put the test back into not run state
    /// </summary>
    bool IsDisposed { get; }

    /// <summary>
    /// Debug lines for displaying information about the test
    /// </summary>
    IReactiveList<string> DebugOutput { get; } 
  }
}