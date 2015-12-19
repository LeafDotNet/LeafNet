using CefSharp;
using CefSharp.Wpf;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      var resourceFactory = browser.ResourceHandlerFactory;

      resourceFactory.GetResourceHandler(browser.WebBrowser, browser.GetBrowser(), browser.GetMainFrame(), null);
    }
  }
}
