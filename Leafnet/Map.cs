using Newtonsoft.Json;

namespace Leafnet
{
  public class Map
  {
    internal readonly string Js = JsVariableNamer.GetNext();
    public Map(string divId, string options = "")
    {
      var script = $"var {Js} = L.map('{divId}')";
      Script.ExecuteAsync(script);
    }

    public Map SetView(LatLng center, int zoom = 8, ZoomPanOptions zoomPanOptions = null)
    {
      var centerJson = JsonConvert.SerializeObject(center);

      var zoomPanOptionsJson = zoomPanOptions == null
        ? "{}" : JsonConvert.SerializeObject(zoomPanOptions);

      var script = $"{Js}.setView({centerJson}, {zoom}, {zoomPanOptionsJson})";
      Script.ExecuteAsync(script);
      return this;
    }

    public Map AddTileLayer(string url, TileLayerOptions tileLayerOptions = null)
    {
      var tileLayerOptionsJson = tileLayerOptions == null
        ? "{}" : JsonConvert.SerializeObject(tileLayerOptions);

      var script =
        $@"L.tileLayer('{url}', {tileLayerOptionsJson}).addTo({Js});";
      Script.ExecuteAsync(script);
      return this;
    }
  }
}
