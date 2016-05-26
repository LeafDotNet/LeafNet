using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CefSharp;

namespace Leafnet
{
  public class TileLayer : JsObject 
  {
    public TileLayerOptions Options { get; }
    public string UrlTemplate { get; private set; }

    public TileLayer( string jsName, IWebBrowser browser, string urlTemplate ) 
      : this( jsName, browser, urlTemplate, TileLayerOptions.Default )
    {
    }

    public TileLayer( string jsName, IWebBrowser browser, 
      string urlTemplate, TileLayerOptions options ) 
      : base(jsName, browser)
    {
      Options = options;
      UrlTemplate = urlTemplate;
    }

    // $"tileLayer.addTo(map);"
    public async Task<TileLayer> AddToMap( Map map)
    { 
      await Evaluate( $"{JsName}.addTo({map.JsName});" );
      return this;
    }

    public async Task<TileLayer> BringToFront()
    {
      await Evaluate( $"{JsName}.bringToFront();" );
      return this;
    }

    public async Task<TileLayer> BringToBack()
    {
      await Evaluate( $"{JsName}.bringToBack();" );
      return this;
    }

    public async Task<TileLayer> SetOpacity( double opacity )
    {
      await Evaluate( $"{JsName}.setOpacity({opacity});" );
      return this;
    }

    public async Task<TileLayer> SetZIndex( int zIndex )
    {
      await Evaluate( $"{JsName}.setZIndex({zIndex});" );
      return this;
    }

    public async Task<TileLayer> ReDraw()
    {
      await Evaluate( $"{JsName}.reDraw();" );
      return this;
    }

    public async Task<TileLayer> SetUrl( string urlTemplate)
    {
      UrlTemplate = urlTemplate;
      await Evaluate( $"{JsName}.setUrl({urlTemplate});" );
      return this;
    }

    public override string ToString()
    {
      return $"tileLayer('{UrlTemplate}', {Options})";
    }
  }
}