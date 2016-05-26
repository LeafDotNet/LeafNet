using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Leafnet
{
  public class L : JsObject
  {

    public L( IWebBrowser browser ) :
      base("L", browser)
    {
    }

    public Task<string> GetVersion()
    {
      return Evaluate( $"{JsName}.version" ).ContinueWith( jsr => jsr.Result.Result.ToString() );
    }

    public async Task<Map> Map( Map map )
    {
      await Evaluate( $"{map.JsName} = {JsName}.map('{map.DivId}');" );
      return map;
    }

    public async Task<Map> Map(string jsName, string divId)
    {
      var map = new Map( jsName, Browser, divId );
      return await Map(map);
    }

    public async Task<TileLayer> TileLayer( string jsName, string url, TileLayerOptions options = null )
    {
      var tileLayer = new TileLayer( jsName, Browser, url, options ?? TileLayerOptions.Default );
      return await TileLayer( tileLayer );
    }

    public async Task<TileLayer> TileLayer( TileLayer tileLayer)
    {
      await Evaluate( $"{tileLayer.JsName} = L.{tileLayer}" );
      return tileLayer;
    }


  }
}
