// ReSharper disable InconsistentNaming

using System.ComponentModel;
using Newtonsoft.Json;

namespace Leafnet
{
  
  public class TileLayerOptions
  {
    public static TileLayerOptions Default = new TileLayerOptions();

    public TileLayerOptions()
    {
      id = "";
      minZoom = 0;
      maxZoom = 18;
      maxNativeZoom = null;
      tileSize = 256;
      subdomains = new[] { "a", "b", "c" };
      errorTileUrl = "";
      attribution = "";
      tms = false;
      continuousWorld = false;
      noWrap = false;
      zoomOffset = 0;
      zoomReverse = false;
      opacity = 1.0;
      zIndex = null;
      unloadInvisibleTiles = true;
      updateWhenIdle = true;
      detectRetina = false;
      reuseTiles = false;
      bounds = null;
    }

    [DefaultValue("")]
    public string id { get; set; }
    [DefaultValue(0)]
    public int minZoom { get; set; }
    [DefaultValue( 18 )]
    public int maxZoom { get; set; }
    [DefaultValue( null )]
    public double? maxNativeZoom { get; set; }
    [DefaultValue( 256 )]
    public int tileSize { get; set; }
    [DefaultValue(new[] { "a", "b", "c" })]
    public string[] subdomains { get; set; }
    [DefaultValue( "" )]
    public string errorTileUrl { get; set; }
    [DefaultValue( "" )]
    public string attribution { get; set; }
    [DefaultValue( false )]
    public bool tms { get; set; }
    [DefaultValue( false )]
    public bool continuousWorld { get; set; }
    [DefaultValue( false )]
    public bool noWrap { get; set; }
    [DefaultValue( 0 )]
    public int zoomOffset { get; set; }
    [DefaultValue( false )]
    public bool zoomReverse { get; set; }
    [DefaultValue( 1.0 )]
    public double opacity { get; set; }
    [DefaultValue( null )]
    public int? zIndex { get; set; }
    [DefaultValue( true )]
    public bool unloadInvisibleTiles { get; set; }
    [DefaultValue( true )]
    public bool updateWhenIdle { get; set; }
    [DefaultValue( false )]
    public bool detectRetina { get; set; }
    [DefaultValue( false )]
    public bool reuseTiles { get; set; }
    [DefaultValue( null )]
    public LatLngBounds bounds { get; set; }

    public override string ToString()
    {
      var serializeSettings = new JsonSerializerSettings();
      serializeSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
      return JsonConvert.SerializeObject( this, serializeSettings );
    }
  }
}