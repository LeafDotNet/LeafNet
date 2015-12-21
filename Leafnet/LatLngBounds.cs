namespace Leafnet
{
  public class LatLngBounds
  {
    public LatLngBounds(LatLng southWest, LatLng northEast)
    {
      SouthWest = southWest;
      NorthEast = northEast;
    }

    public LatLng SouthWest { get; }
    public LatLng NorthEast { get; }
  }
}
