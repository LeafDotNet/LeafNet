using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Leafnet
{
  public class L
  {
    private readonly IWebBrowser _browser;

    public L( IWebBrowser browser )
    {
      _browser = browser;
    }



    public string GetVersion()
    {
      var script = Script.EvaluateJavaScript( "L.version", _browser ).Result;
      return script;
    }
  }
}
