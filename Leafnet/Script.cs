using System;
using System.Threading.Tasks;
using CefSharp;

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
  }
}
