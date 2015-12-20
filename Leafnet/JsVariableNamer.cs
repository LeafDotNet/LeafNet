namespace Leafnet
{
  public static class JsVariableNamer
  {
    public static string VarPrefix = "v";
    public static int CurrentIndex = 0;

    public static string GetNext()
    {
      return string.Format("{0}{1}", VarPrefix, CurrentIndex++);
    }
  }
}
