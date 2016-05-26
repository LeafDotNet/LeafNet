using System;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using Newtonsoft.Json;

namespace Leafnet
{
  public static class Script
  {
    static Action<string> executeAsync;
    static Func<string, Task<JavascriptResponse>> evaluateAsync; 

    public static void Initialize(
      Action<string> executeScriptAsync, 
      Func<string, Task<JavascriptResponse>> evaluateScriptAsync)
    {
      executeAsync = executeScriptAsync;
      evaluateAsync = evaluateScriptAsync;
    }

    public static void ExecuteAsync(string script)
    {
      if (executeAsync == null)
        throw new NullReferenceException("webBrowser");

      executeAsync(script);
    }

    public static async Task<T> EvaluateAsync<T>(string script)
    {
      if (evaluateAsync == null)
        throw new NullReferenceException("webBrowser");

      var response = await evaluateAsync(script);
      if (response.Result == null)
        return default(T);

      return (T) response.Result;
    }

    public static async Task<string> EvaluateJavaScript( string s, IWebBrowser webBrowser )
    {
      try
      {
        var response = await webBrowser.EvaluateScriptAsync( s );
        if ( response.Success && response.Result is IJavascriptCallback )
        {
          response = await ( (IJavascriptCallback)response.Result ).ExecuteAsync( "This is a callback from EvaluateJavaScript" );
        }
        return response.Success ? ( JsonConvert.SerializeObject( response.Result, Formatting.Indented ) ?? "null" ) : response.Message;
      }
      catch ( Exception e )
      {
        MessageBox.Show( "Error while evaluating Javascript: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error );
      }
      return "exception";
    }
  }
}
