using System.Collections.ObjectModel;
using System.Reactive;
using Leafnet.Wpf.Tests.MapTest.UnitTests;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest
{
  public class JsUnitTestsViewModel : ReactiveObject
  {
    

    public JsUnitTestsViewModel()
    {
      UnitTests = new ReactiveList<IJsUnitTest>();
      RunTests = ReactiveCommand.CreateAsyncTask( async _ => {
        foreach ( var jsUnitTest in UnitTests )
        { // currently we need to run each of these in a row so we can tag on tests 
          // that go together
          await jsUnitTest.RunTest();
        }
      } );
    }

    public IReactiveList<IJsUnitTest> UnitTests { get; }

    private IJsUnitTest _selectedTest;
    public IJsUnitTest SelectedTest
    {
      get { return _selectedTest; }
      set { this.RaiseAndSetIfChanged( ref _selectedTest, value ); }
    }

    public IReactiveCommand<Unit> RunTests { get; }
  }
}
