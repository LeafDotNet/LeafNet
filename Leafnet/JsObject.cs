using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CefSharp;

namespace Leafnet
{
  public abstract class JsObject
  {
    protected JsObject(string jsName, IWebBrowser browser)
    {
      Browser = browser;
      JsName = jsName;
    }

    public string JsName { get; }

    public async Task<JavascriptResponse> Evaluate(string script)
    {
      var response = await Browser.EvaluateScriptAsync( script, TimeSpan.FromMilliseconds( 500 ) );
        if ( !response.Success )
          throw new ScriptException( $"JS: {script} | Message: {response.Message}" );
      return response;
    }

    protected IWebBrowser Browser { get; }
  }
}