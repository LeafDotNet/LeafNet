using CefSharp.Wpf;
using System;
using CefSharp;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Leafnet
{
  public class LatLng
  {
    readonly JObject js;
    private LatLng(JObject js)
    {
      this.js = js;
    }

    public static async Task<LatLng> New(ChromiumWebBrowser browser, double lat, double lon)
    {
      var script = string.Format("var latLng = L.latLng({0}, {1}); JSON.stringify(latLng);", lat, lon);
      var response = await browser.EvaluateScriptAsync(script);
      var js = JObject.FromObject(response.Result);
      return new LatLng(js);
    }

    public async Task<double> DistanceTo(LatLng otherLatLng)
    {
      var script = js["distanceTo"];

      return 0;
    }

    public override bool Equals(object obj)
    {
      throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }

    public LatLng Wrap(double left, double right)
    {
      throw new NotImplementedException();
    }
  }
}
