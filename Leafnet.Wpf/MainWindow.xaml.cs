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

    private async void OnFrameLoadEnd(object _, FrameLoadEndEventArgs e)
    {
      if (!string.Equals(Address, e.Url, StringComparison.InvariantCultureIgnoreCase))
        return;

      browser.LoadMapAtLocationAndZoom(47.6097, -122.3331);
      browser.LoadTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");

      var l1 = await LatLng.New(browser, 47.6097, -122.3331);
      var l2 = await LatLng.New(browser, 47.0097, -122.3331);

      var d = l1.DistanceTo(l2);
    }
  }
}
