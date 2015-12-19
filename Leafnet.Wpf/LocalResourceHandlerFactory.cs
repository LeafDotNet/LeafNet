using CefSharp;

namespace Leafnet.Wpf
{
  internal class LocalSchemeHandlerFactory : ISchemeHandlerFactory
  {
    public const string SchemeName = "Local";

    public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
    {
      return new LocalResourceHandler();
    }
  }
}