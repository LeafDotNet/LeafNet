using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest.UnitTests
{
  public class LeafnetUnitTest<TInput, TResult> : ReactiveObject, IJsUnitTest
  {
    private readonly Func<Task<TInput>> _execScript;
    private readonly Func<Task<TResult>> _evalScript;
    private readonly Func<TResult, bool> _equalsCheck;
    public TInput Item;
    public TResult ResponseItem;

    public LeafnetUnitTest( string description, 
      Func<Task<TInput>> execScript, 
      Func<Task<TResult>> evalScript, 
      Func<TResult,bool> equalsCheck )
    {
      _execScript = execScript;
      _evalScript = evalScript;
      _equalsCheck = equalsCheck;
      Description = description;
    }

    public async Task RunTest()
    {
      State = TestStates.Running;
      Item = await _execScript();
      ResponseItem = await _evalScript();
      State = _equalsCheck(ResponseItem) ? TestStates.Passed : TestStates.Failed;
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

  public class LeafnetBasicUnitTest<TResult> : ReactiveObject, IJsUnitTest
  {
    private readonly Func<Task<TResult>> _execScript;
    private readonly Func<TResult, bool> _equalsCheck;
    public TResult ResponseItem;

    public LeafnetBasicUnitTest( string description,
      Func<Task<TResult>> execScript )
      : this(description, execScript, _ => true)
    {
    }

    public LeafnetBasicUnitTest( string description,
      Func<Task<TResult>> execScript,
      Func<TResult, bool> equalsCheck )
    {
      _execScript = execScript;
      _equalsCheck = equalsCheck;
      Description = description;
    }

    public async Task RunTest()
    {
      State = TestStates.Running;
      ResponseItem = await _execScript();
      State = _equalsCheck( ResponseItem ) ? TestStates.Passed : TestStates.Failed;
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
