using CefSharp;
using CefSharp.Wpf;

namespace Leafnet.Wpf
{
  public static class Leaflet
  {
    public static void LoadMapAtLocationAndZoom(this ChromiumWebBrowser browser, double lat, double lon, int zoom = 8)
    {
      var script = $"var map = L.map('map').setView([{lat}, {lon}], {zoom});";
      browser.ExecuteScriptAsync(script);
    }

    public static void LoadTileLayer(this ChromiumWebBrowser browser, string tileLayerUrl, int maxZoom = 18)
    {
      var script =
        $@"L.tileLayer('{tileLayerUrl}', {{
            maxZoom: {maxZoom},
          }}).addTo(map);";
      browser.ExecuteScriptAsync(script);
    }
  }
}