namespace Leafnet.Wpf
{
  public static class StringExtensions
  {
    public static string TrimStart(this string s, string trim)
    {
      return s.TrimStart(trim.ToCharArray());
    }
  }
}
