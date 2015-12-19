using CefSharp;
using CefSharp.Wpf;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      browser.Address = "custom://web/index.html";
    }
  }
}
