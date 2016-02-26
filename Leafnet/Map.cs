using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

    public async Task<bool> HasLayer( TileLayer layer )
    {
      var js = $"{JsName}.hasLayer({layer.JsName});";
      var result = await Evaluate( js );
      return (bool) result.Result;
    }

    public string DivId { get; }
  }  
}
