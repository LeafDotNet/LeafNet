namespace Leafnet
{
  public class LatLngBounds
  {
    internal readonly string Js;
    public LatLngBounds(LatLng southWest, LatLng northEast)
    {
      Js = JsVariableNamer.GetNext();
      SouthWest = southWest;
      NorthEast = northEast;
      var script = $"var {Js} = L.latLngBounds({southWest}, {northEast})";
      Script.ExecuteAsync(script);
    }

    public LatLng SouthWest { get; }
    public LatLng NorthEast { get; }
  }
}
