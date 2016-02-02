using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace Leafnet.Wpf.Tests
{
  public class LeafletTests
  {
    public static void TestLBeingInitialized()
    {
      var window = new Window();
      var webBrowser = new ChromiumWebBrowser()
      {
        Address = Path.GetFullPath( "Web//index.html" )
      };
      window.Content = webBrowser;
      window.Show();      
      webBrowser.FrameLoadEnd += delegate
      {
        var result = Assert( webBrowser, "4", "2+2" );
      };
    }

    public static async Task<bool> Assert(IWebBrowser browser, string expected, string javascript )
    {
      var script = browser.EvaluateScriptAsync( "{ value = 2+2 }" );
      await script;
      return script.Result.Message == expected;
    }
  }
}
