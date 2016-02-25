using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest
{
  public class LeafnetActionUnitTest<T,T2> : ReactiveObject
  {
    private readonly IWebBrowser _browser;
    private readonly Func<Task<T>> _execution;
    private readonly Func<T, string> _evalScript;
    private readonly Func<T2, bool> _evaluateResult;
    public T Item;
    public T2 ResponseItem;

    public LeafnetActionUnitTest(IWebBrowser browser, Func<Task<T>> execution, Func<T,string> evalScript, Func<T2,bool> evaluateResult )
    {
      _browser = browser;
      _execution = execution;
      _evalScript = evalScript;
      _evaluateResult = evaluateResult;
    }

    public async Task RunTest()
    {
      State = TestStates.Running;
      Item = await _execution();
      var evalResponse = await _browser.EvaluateScriptAsync( _evalScript(Item) );
      if( evalResponse.Result == null)
      {
        State = TestStates.Failed;
        return;
      }

      var serializeSettings = new JsonSerializerSettings();
      serializeSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
      ActualJavaScriptResult = JsonConvert.SerializeObject( evalResponse.Result, serializeSettings );

      ResponseItem = JsonConvert.DeserializeObject<T2>( ActualJavaScriptResult );
      State = _evaluateResult( ResponseItem ) ? TestStates.Passed : TestStates.Failed;
    }

    public string ActualJavaScriptResult { get; set; }

    public TestStates State { get; set; }
  }
}
