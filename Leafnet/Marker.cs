using Newtonsoft.Json;

namespace Leafnet
{
  public class Marker
  {
    internal readonly string Js = JsVariableNamer.GetNext();
    public Marker(LatLng latLng, MarkerOptions markerOptions = null)
    {
      this.latLng = latLng;
      this.options = markerOptions;
    }

    public Marker addTo(Map map)
    {
      var latLngJson = JsonConvert.SerializeObject(latLng);
      var markerOptionsJson = options == null
        ? "{}" : JsonConvert.SerializeObject(options);

      var script = $"var {Js} = L.marker({latLngJson}, {markerOptionsJson}).addTo({map.Js})";
      Script.ExecuteAsync(script);

      return this;
    }

    public LatLng latLng;
    public MarkerOptions options;
  }
}
