using System;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;

namespace Leafnet
{
  public static class Script
  {
    static ChromiumWebBrowser webBrowser = null;

    public static void Initialize(ChromiumWebBrowser browser)
    {
      webBrowser = browser;
    }

    public static void ExecuteAsync(string script)
    {
      if (webBrowser == null)
        throw new NullReferenceException("webBrowser");

      webBrowser.ExecuteScriptAsync(script);
    }

    public static async Task<T> EvaluateAsync<T>(string script)
    {
      if (webBrowser == null)
        throw new NullReferenceException("webBrowser");

      var response = await webBrowser.EvaluateScriptAsync(script);
      if (response.Result == null)
        return default(T);

      return (T) response.Result;
    }
  }
}
