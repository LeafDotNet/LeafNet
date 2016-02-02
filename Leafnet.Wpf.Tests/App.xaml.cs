using CefSharp;

namespace Leafnet.Wpf.Tests
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    public App()
    {
      var settings = new CefSettings();
      settings.SetOffScreenRenderingBestPerformanceArgs();
      Cef.Initialize( settings );
    }
  }
}
