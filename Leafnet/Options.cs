namespace Leafnet
{
  public class PanOptions
  {
    public bool animate;
    public double duration;
    public double easeLinearity;
    public bool noMoveStart;
  }

  public class ZoomOptions
  {
    bool animate;
  }

  public class ZoomPanOptions
  {
    bool reset;
    PanOptions pan;
    ZoomOptions zoom;
    bool animate;
  }

  public class TileLayerOptions
  {
    public int minZoom;
    public int maxZoom;
    public double maxNativeZoom;
    public double tileSize;
    public string[] subdomains;
    public string errorTileUrl;
    public string attribution;
    public bool tms;
    public bool continuousWorld;
    public bool noWrap;
    public int zoomOffset;
    public bool zoomReverse;
    public double opacity;
    public int zIndex;
    public bool unloadInvisibleTiles;
    public bool updateWhenIdle;
    public bool detectRetina;
    public bool reuseTiles;
    public LatLngBounds bounds;
  }

  public class IconOptions
  {

    public string iconUrl;
    public string iconRetinaUrl;
    public Leafnet.Point iconSize;
    public Leafnet.Point iconAnchor;
    public string shadowUrl;
    public string shadowRetinaUrl;
    public Leafnet.Point shadowSize;
    public Leafnet.Point shadowAnchor;
    public Leafnet.Point popupAnchor;
    public string className;
  }

  public class MarkerOptions
  {
    public Icon icon;
    public bool clickable;
    public bool draggable;
    public bool keyboard;
    public string title;
    public string alt;
    public int zIndexOffset;
    public double opacity;
    public bool riseOnHover;
    public int riseOffset;
  }
}
