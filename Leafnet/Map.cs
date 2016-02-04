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

    public JsTask RemoveLayerJs( TileLayer layer )
    {
      var js = $"{JsName}.removeLayer({layer.JsName});";
      return new JsTask( js, () => Browser.EvaluateScriptAsync( js) );
    }


    public async Task<Map> RemoveLayerJsd( TileLayer layer )
    {
      await RemoveLayerJs( layer ).Run();
      return this;
    }

    public string DivId { get; }
  }  

  public class JsTask
  {
    public string Js { get; }

    public JavascriptResponse Response { get; set; }
    private readonly Func<Task<JavascriptResponse>> _runJs;

    public JsTask( string js, Func<Task<JavascriptResponse>> response )
    {
      Js = js;
      _runJs = response;
    }

    public async Task<JavascriptResponse> Run()
    {
      return Response = await _runJs.Invoke();
    }
  }
}
