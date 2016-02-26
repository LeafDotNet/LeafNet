using System;
using CefSharp;
using CefSharp.Wpf;
using ReactiveUI;

namespace Leafnet.Wpf.Tests
{
  public class BrowserViewModel : ReactiveObject, IDisposable
  {
    private IWpfWebBrowser _webBrowser;
    public const string Address = "Web//index.html";
    public L L;

    public BrowserViewModel()
    {
      var command = ReactiveCommand.Create();
      command.Subscribe( _ => _webBrowser?.ShowDevTools() );
      ShowDevMode = command;
    }

    public void Dispose()
    {
      if ( _webBrowser == null ) return;
      _webBrowser.FrameLoadEnd -= BrowserOnFrameLoadEnd;
      _webBrowser.Dispose();
    }

    public IWpfWebBrowser WebBrowser
    {
      get { return _webBrowser; }
      set
      {
        if ( this.RaiseAndSetIfChanged( ref _webBrowser, value ) != null )
        {
          L = new L( _webBrowser );
          _webBrowser.FrameLoadEnd += BrowserOnFrameLoadEnd;
        }
      }
    }

    private void BrowserOnFrameLoadEnd( object _, FrameLoadEndEventArgs __ )
    {
      Script.Initialize( _webBrowser.ExecuteScriptAsync, s => _webBrowser.EvaluateScriptAsync( s ) );

//      var map = new Map( "map" )
//        .SetView( new LatLng( 47.6097, -122.3331 ) )
//        .AddTileLayer( @"http://{s}.tile.osm.org/{z}/{x}/{y}.png" );
//
//      var marker = new Marker( new LatLng( 47.6097, -122.3331 ) );
//      marker.AddTo( map );      
    }

    public IReactiveCommand ShowDevMode { get; set; }
  }
}