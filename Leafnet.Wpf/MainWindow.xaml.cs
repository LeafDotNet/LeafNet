using System;
using CefSharp;
using System.IO;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public const string Address = "Web//index.html";

    public MainWindow()
    {
      InitializeComponent();

      Closing += OnClosing;
      browser.Address = Path.GetFullPath(Address);
      browser.FrameLoadEnd += OnFrameLoadEnd;
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      browser.FrameLoadEnd -= OnFrameLoadEnd;
      browser.Dispose();
    }

    private void OnFrameLoadEnd(object _, FrameLoadEndEventArgs e)
    {
      Script.Initialize(browser.ExecuteScriptAsync, s => browser.EvaluateScriptAsync(s));

      var map = new Map("map")
        .SetView(new LatLng(47.6097, -122.3331))
        .AddTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");

      var marker = new Marker(new LatLng(47.6097, -122.3331));
      marker.addTo(map);
    }
  }
}
