using System;
using System.Collections.Generic;
using System.Linq;
using Leafnet.Wpf.Tests.Util;

namespace Leafnet.Wpf.Tests
{
  public class UnitTest : ViewModel
  {
    private PassState _passed = PassState.NotRun;
    private string _javaScript;
    private string _expectedResult;
    private string _actualResult;

    public UnitTest(string javaScript, string expectedResult)
    {
      JavaScript = javaScript;
      ExpectedResult = expectedResult;
    }

    public string ExpectedResult
    {
      get { return _expectedResult; }
      set { SetField( ref _expectedResult, value ); }
    }

    public string ActualResult
    {
      get { return _actualResult; }
      set { SetField( ref _actualResult, value ); }
    }

    public string JavaScript
    {
      get { return _javaScript; }
      set { SetField( ref _javaScript, value ); }
    }

    public PassState Passed
    {
      get { return _passed; }
      set { SetField( ref _passed, value ); }
    }
  }
}