using System;
using System.Threading.Tasks;

namespace Leafnet
{
  public class LatLng
  {
    internal readonly string Js;
    public LatLng(double lat, double lon)
    {
      Js = JsVariableNamer.GetNext();
      var script = string.Format("var {0} = L.latLng({1}, {2})", Js, lat, lon);
      Script.ExecuteAsync(script);
    }

    public async Task<double> DistanceTo(LatLng other)
    {
      var script = string.Format("{0}.distanceTo({1})", Js, other.Js);
      var result = await Script.EvaluateAsync<double?>(script);
      return result ?? 0;
    }

    public override bool Equals(object obj)
    {
      var other = obj as LatLng;
      if (other == null)
        return false;

      var script = string.Format("{0}.equals({1})", Js, other.Js);
      var result = Script.EvaluateAsync<bool?>(script).Result;
      return result ?? false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }

    public LatLng Wrap(double left, double right)
    {
      throw new NotImplementedException();
    }
  }
}
