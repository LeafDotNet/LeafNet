using System;
using System.Threading.Tasks;

namespace Leafnet
{
  public class LatLng
  {
    internal readonly string Js;
    public LatLng(double lat, double lng)
    {
      Js = JsVariableNamer.GetNext();
      Lat = lat;
      Lng = lng;
      var script = $"var {Js} = L.latLng({lat}, {lng})";
      Script.ExecuteAsync(script);
    }

    public async Task<double> DistanceTo(LatLng other)
    {
      var script = $"{Js}.distanceTo({other.Js})";
      var result = await Script.EvaluateAsync<double?>(script);
      return result ?? 0;
    }

    public override bool Equals(object obj)
    {
      var other = obj as LatLng;
      if (other == null)
        return false;

      var margin = Math.Max(
            Math.Abs(Lat - other.Lat),
            Math.Abs(Lng - other.Lng));

      return margin <= 1.0E-9;
    }

    public override int GetHashCode()
    {
      return Lat.GetHashCode() + Lng.GetHashCode();
    }

    public double Lat { get; }
    public double Lng { get; }
  }
}
