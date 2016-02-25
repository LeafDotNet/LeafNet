using System;
using System.Collections;
using System.Reactive;
using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest
{
  public class JsUnitTest : ReactiveObject
  {
    private readonly IWebBrowser _browser;

    public JsUnitTest( IWebBrowser browser, string description, string inputJs, string evaluateJs, string expectedJavaScriptResult )
    {
      _browser = browser;
      Description = description;
      InputJs = inputJs;
      EvaluateJs = evaluateJs;
      ExpectedJavaScriptResult = expectedJavaScriptResult;
      RunCommand = ReactiveCommand.CreateAsyncTask( async _ => await RunTest());
    }

    public ReactiveCommand<Unit> RunCommand { get; }

    public async Task RunTest()
    {
      InputState = TestStates.Running;
      var response = await _browser.EvaluateScriptAsync( InputJs );
      InputState = response.Success ? TestStates.Passed : TestStates.Failed;
      InputJsResult = response.Result?.ToString() ?? "";
      if ( InputState == TestStates.Failed )
        return;
      var evalResponse = await _browser.EvaluateScriptAsync( EvaluateJs );
      if(evalResponse.Result is IDictionary)
      {
        var serializeSettings = new JsonSerializerSettings();
        serializeSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        ActualJavaScriptResult = JsonConvert.SerializeObject( evalResponse.Result, serializeSettings );
      } else if ( evalResponse.Result != null )
        ActualJavaScriptResult = evalResponse.Result.ToString();
      EvalutionState = ActualJavaScriptResult == ExpectedJavaScriptResult ? TestStates.Passed : TestStates.Failed;
    }

    public string Description { get; }

    public string InputJs { get; }

    public string EvaluateJs { get; }

    public string ExpectedJavaScriptResult { get; }

    string _inputJsResult;
    public string InputJsResult
    {
      get { return _inputJsResult; }
      set { this.RaiseAndSetIfChanged( ref _inputJsResult, value ); }
    }

    string _actualJavaScriptResult;
    public string ActualJavaScriptResult
    {
      get { return _actualJavaScriptResult; }
      set { this.RaiseAndSetIfChanged( ref _actualJavaScriptResult, value ); }
    }

    private TestStates _inputState;
    public TestStates InputState
    {
      get { return _inputState; }
      set { this.RaiseAndSetIfChanged( ref _inputState, value ); }
    }

    private TestStates _evalutionState;
    public TestStates EvalutionState
    {
      get { return _evalutionState; }
      set { this.RaiseAndSetIfChanged( ref _evalutionState, value ); }
    }
  }
}
