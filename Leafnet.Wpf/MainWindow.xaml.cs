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

      Script.Initialize(browser);

      browser.LoadMapAtLocationAndZoom(47.6097, -122.3331);
      browser.LoadTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");

      var l1 = new LatLng(47.6097, -122.3331);
      var l2 = new LatLng(47.5097, -122.3331);

      var distance = await l1.DistanceTo(l2);
      var equals = l1.Equals(l2);
    }
  }
}
