using CefSharp;

namespace Leafnet.Wpf
{
  public partial class App
  {
    public App()
    {
      var settings = new CefSettings();
      settings.RegisterScheme(new CefCustomScheme
      {
        SchemeName = LocalSchemeHandlerFactory.SchemeName,
        SchemeHandlerFactory = new LocalSchemeHandlerFactory()
      });
      Cef.Initialize(settings);
    }
  }
}
