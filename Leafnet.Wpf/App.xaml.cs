using CefSharp;

namespace Leafnet.Wpf
{
  public partial class App
  {
    public App()
    {
      var settings = new CefSettings();
      settings.SetOffScreenRenderingBestPerformanceArgs();

      Cef.Initialize(settings);
    }
  }
}
