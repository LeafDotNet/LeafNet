using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;

namespace Leafnet
{
  public class Map : JsObject
  {
    public Map(string jsName, IWebBrowser browser, string divId) :
      base (jsName, browser)
    {
      DivId = divId;
    }

    public async Task<Map> RemoveLayer( TileLayer layer)
    {
      var js = $"{JsName}.removeLayer({layer.JsName});";
      await Evaluate( js );
      return this;
    } 

    public string DivId { get; }
  }
}
