using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest
{
  public class JsUnitTestsViewModel : ReactiveObject
  {
    public JsUnitTestsViewModel()
    {
      UnitTests = new ReactiveList<JsUnitTest>();
      RunTests = ReactiveCommand.CreateAsyncTask( async _ => {
        foreach ( var jsUnitTest in UnitTests )
        { // currently we need to run each of these in a row so we can tag on tests 
          // that go together
          await jsUnitTest.RunTest();
        }
      } );
    }

    public IReactiveList<JsUnitTest> UnitTests { get; }

    public IReactiveCommand<Unit> RunTests { get; }
  }
}
