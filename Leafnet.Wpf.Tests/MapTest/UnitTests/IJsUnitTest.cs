using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace Leafnet.Wpf.Tests.MapTest.UnitTests
{
  public interface IJsUnitTest : IReactiveObject
  {
    Task RunTest();
    string Description { get; }
    string ActualJavaScriptResult { get; set; }
    TestStates State { get; set; }
  }
}