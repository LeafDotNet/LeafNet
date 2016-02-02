using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using Leafnet.Wpf.Tests.Util;

namespace Leafnet.Wpf.Tests
{
  public class BrowserViewModel : ViewModel, IDisposable
  {
    private IWpfWebBrowser _webBrowser;
    public const string Address = "Web//index.html";
    public L L;

    public BrowserViewModel()
    {
      ShowDevMode = new DelegateCommand( () => _webBrowser?.ShowDevTools() );
      
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
        if ( SetField( ref _webBrowser, value ) )
        {
          L = new L( _webBrowser );
          _webBrowser.FrameLoadEnd += BrowserOnFrameLoadEnd;
        }
      }
    }

    private void BrowserOnFrameLoadEnd( object _, FrameLoadEndEventArgs __ )
    {
      Script.Initialize( _webBrowser.ExecuteScriptAsync, s => _webBrowser.EvaluateScriptAsync( s ) );

      var map = new Map( "map" )
        .SetView( new LatLng( 47.6097, -122.3331 ) )
        .AddTileLayer( @"http://{s}.tile.osm.org/{z}/{x}/{y}.png" );

      var marker = new Marker( new LatLng( 47.6097, -122.3331 ) );
      marker.AddTo( map );      
    }

    public ICommand ShowDevMode { get; set; }
  }
}