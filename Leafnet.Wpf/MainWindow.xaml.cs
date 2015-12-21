using System;
using CefSharp;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public const string Address = "custom://web/index.html";

    public MainWindow()
    {
      InitializeComponent();

      Closing += OnClosing;
      browser.Address = Address;
      browser.FrameLoadEnd += OnFrameLoadEnd;
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      browser.FrameLoadEnd -= OnFrameLoadEnd;
      browser.Dispose();
    }

    private void OnFrameLoadEnd(object _, FrameLoadEndEventArgs e)
    {
      if (!string.Equals(Address, e.Url, StringComparison.InvariantCultureIgnoreCase))
        return;

      Script.Initialize(browser.ExecuteScriptAsync, s => browser.EvaluateScriptAsync(s));

      var map = new Map("map")
        .SetView(new LatLng(47.6097, -122.3331))
        .AddTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");
    }
  }
}
