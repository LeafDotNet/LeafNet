using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest.UnitTests
{
  public class JsFunctionTest<T,T2> : ReactiveObject, IJsUnitTest
  {
    private readonly IWebBrowser _browser;
    private readonly Func<Task<T>> _execution;
    private readonly Func<T, string> _evalScript;
    private readonly T2 _expectedResult;
    public T Item;
    public T2 ResponseItem;

    public JsFunctionTest(IWebBrowser browser, string description, Func<Task<T>> execution, Func<T,string> evalScript, T2 expectedResult )
    {
      _browser = browser;
      Description = description;
      _execution = execution;
      _evalScript = evalScript;
      _expectedResult = expectedResult;
    }

    public async Task RunTest()
    {
      State = TestStates.Running;
      Item = await _execution();
      var jsEval = _evalScript( Item );
      var evalResponse = await _browser.EvaluateScriptAsync( jsEval );
      if( evalResponse.Result == null)
      {
        State = TestStates.Failed;
        return;
      }

      var serializeSettings = new JsonSerializerSettings();
      serializeSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

      var expected = JsonConvert.SerializeObject( _expectedResult, serializeSettings );
      var temp = JsonConvert.SerializeObject( evalResponse.Result );
      var actualObject = JsonConvert.DeserializeObject<T2>( temp );
      ActualJavaScriptResult = JsonConvert.SerializeObject( actualObject, serializeSettings );

      State = ActualJavaScriptResult == expected ? TestStates.Passed : TestStates.Failed;
    }

    public string Description { get; }

    private string _actualJavaScriptResult;
    public string ActualJavaScriptResult
    {
      get { return _actualJavaScriptResult; }
      set { this.RaiseAndSetIfChanged( ref _actualJavaScriptResult, value ); }
    }

    private TestStates _state;
    public TestStates State
    {
      get { return _state; }
      set { this.RaiseAndSetIfChanged( ref _state, value ); }
    }
  }
}
