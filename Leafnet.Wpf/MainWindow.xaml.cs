using System;
using CefSharp;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public const string Address = "Web//index.html";

    private readonly IWebBrowser _browser;

    public MainWindow()
    {
      InitializeComponent();

      Closing += OnClosing;
      browser.Address = Path.GetFullPath(Address);
      browser.FrameLoadEnd += OnFrameLoadEnd;
      KeyDown += OnKeyDown;
      _browser = browser;
    }

    private void OnKeyDown( object sender, KeyEventArgs e )
    {
      if ( e.Key == Key.F12 )
        _browser.ShowDevTools();
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      browser.FrameLoadEnd -= OnFrameLoadEnd;
      KeyDown -= OnKeyDown;
      browser.Dispose();
    }

    private void OnFrameLoadEnd(object _, FrameLoadEndEventArgs e)
    {
      //      Script.Initialize(browser.ExecuteScriptAsync, s => browser.EvaluateScriptAsync(s));
      //      var l = new L( browser );



//      l.Execute( "var map = L.map('map').setView([51.505, -0.09], 13);" );
//      var map = l.Map( "map", "map" );

//      var map = new Map("map")
//        .SetView(new LatLng(47.6097, -122.3331))
//        .AddTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");
//
//      var marker = new Marker(new LatLng(47.6097, -122.3331));
//      marker.AddTo(map);
    }
  }
}
