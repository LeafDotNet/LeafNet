
namespace Leafnet
{
  public class Marker
  {
    internal readonly string Js = JsVariableNamer.GetNext();
    public Marker(LatLng latLng, MarkerOptions options = null)
    {
      this.latLng = latLng;
      this.options = options;

      var script = $"var {Js} = L.map('{divId}')";
      Script.ExecuteAsync(script);
    }
    public LatLng latLng;
    public MarkerOptions options;
  }
}
