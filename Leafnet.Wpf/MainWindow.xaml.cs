using System;
using CefSharp;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public const string Address = "Web//index.html";

    private readonly IWebBrowser _browser;

    public MainWindow()
    {
      InitializeComponent();

      Closing += OnClosing;
      browser.Address = Path.GetFullPath(Address);
      browser.FrameLoadEnd += OnFrameLoadEnd;
      KeyDown += OnKeyDown;
      _browser = browser;
    }

    private void OnKeyDown( object sender, KeyEventArgs e )
    {
      if ( e.Key == Key.F12 )
        _browser.ShowDevTools();
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      browser.FrameLoadEnd -= OnFrameLoadEnd;
      KeyDown -= OnKeyDown;
      browser.Dispose();
    }

    private void OnFrameLoadEnd(object _, FrameLoadEndEventArgs e)
    {
      //      Script.Initialize(browser.ExecuteScriptAsync, s => browser.EvaluateScriptAsync(s));
      //      var l = new L( browser );
      ThisNeedsToBeFiguredOut( _browser );



//      l.Execute( "var map = L.map('map').setView([51.505, -0.09], 13);" );
//      var map = l.Map( "map", "map" );

//      var map = new Map("map")
//        .SetView(new LatLng(47.6097, -122.3331))
//        .AddTileLayer(@"http://{s}.tile.osm.org/{z}/{x}/{y}.png");
//
//      var marker = new Marker(new LatLng(47.6097, -122.3331));
//      marker.AddTo(map);
    }

    private static async void RunScriptsDefault(IWebBrowser browser)
    {
      browser.ExecuteScriptAsync( "map = L.map('map');" );
      browser.ExecuteScriptAsync( "map.setView([51.505, -0.09], 13);" );
      var url = @"https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw";
      browser.ExecuteScriptAsync( $"tileLayer = L.tileLayer('{url}', {"{id: 'mapbox.streets'}"})" );
      browser.ExecuteScriptAsync( $"tileLayer.addTo(map);" );
    }

    private async static void ThisNeedsToBeFiguredOut( IWebBrowser browser )
    {
      //      var script = "map = L.map('map');";
      // fails 
//      var result = browser.EvaluateScriptAsync( "map = L.map('map');" ).Result;
//        Console.WriteLine();
//        Task<JavascriptResponse> result = browser.EvaluateScriptAsync( "map = L.map('map');" );
//        result.ContinueWith( jsr => Console.WriteLine());
      var l = new L( browser );
      var map = await l.Map( "map", "map" );
      await l.Evaluate( "map.setView([51.505, -0.09], 13);" );
      var url = @"https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw";
      var tileLayer = await l.TileLayer( "tileLayer", url, new TileLayerOptions { id = "mapbox.streets" } );
      await tileLayer.AddToMap( map );
      await tileLayer.SetOpacity( 0.7 );
      await Task.Delay( 2000 );

      var task = map.RemoveLayerJs( tileLayer );
      Console.WriteLine( task.Js );
      await task.Run();
      Console.WriteLine(  task.Response.Success == true );
      var jsUnit = new JsUnitTest( browser, "map.removeLayer(tileLayer)", "map.hasLayer(tileLayer)", "False" );
      await jsUnit.RunTest();
      Console.WriteLine(  );

      //      l.Execute( "map.setView([51.505, -0.09], 13);" );

      //      l.TileLayer("tileLayer", url, new TileLayerOptions { id = "mapbox.streets" }).AddToMap(map);

      //      browser.ExecuteScriptAsync( $"L.tileLayer('{url}', {"{id: 'mapbox.streets'}"}).addTo(map);" );

      //      var result = browser.EvaluateScriptAsync( script ).Result;
      //      var result = browser.EvaluateScriptAsync( "map = L.map('map');", TimeSpan.FromMilliseconds( 500 ) ).Result;
    }

    public enum TestStates
    {
      NotRunning,
      Running,
      Passed,
      Failed
    }

    public class JsUnitTest
    {
      private readonly IWebBrowser _browser;
      public string InputJs;
      public string InputJsResult;

      public string EvaluateJs;
      public string ExpectedJavaScriptResult;
      public string ActualJavaScriptResult { get; set; }

      public TestStates InputState { get; set; }
      public TestStates CheckState { get; set; }
      

      public JsUnitTest( IWebBrowser browser, string inputJs, string evaluateJs, string expectedJavaScriptResult )
      {
        _browser = browser;
        InputJs = inputJs;
        EvaluateJs = evaluateJs;
        ExpectedJavaScriptResult = expectedJavaScriptResult;
        InputState= TestStates.NotRunning;
        CheckState = TestStates.NotRunning;
      }

      public async Task RunTest()
      {
        InputState = TestStates.Running;
        var response = await _browser.EvaluateScriptAsync( InputJs );
        InputState = response.Success ? TestStates.Passed : TestStates.Failed;
        InputJsResult = response.Result?.ToString() ?? "";
        if ( InputState == TestStates.Failed )
          return;
        var evalResponse = await _browser.EvaluateScriptAsync( EvaluateJs );        
        ActualJavaScriptResult = evalResponse.Result?.ToString() ?? "";
        CheckState = ActualJavaScriptResult == ExpectedJavaScriptResult ? TestStates.Passed : TestStates.Failed;
      }
    }
  }
}
