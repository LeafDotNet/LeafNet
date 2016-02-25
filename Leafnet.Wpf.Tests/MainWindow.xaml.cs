using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Leafnet.Wpf.Tests.MapTest;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using ReactiveUI;

namespace Leafnet.Wpf.Tests
{
  public partial class MainWindow : MetroWindow
  {
    public const string Address = "Web//index.html";

    public MainWindow()
    {
      InitializeComponent();
      _browser.Address = Path.GetFullPath( "Web//index.html" );
      _browser.FrameLoadEnd += delegate
      {
        Dispatcher.InvokeAsync( async () =>
        {
//          var viewmodel = new UnitTestsViewModel( _browser, Dispatcher );
//          DataContext = viewmodel;
//          viewmodel.RunUnitTests();
          var viewmodel = new JsUnitTestsViewModel();
          _jsunitTests.DataContext = viewmodel;

          var defaultTest = new JsUnitTest( _browser, "Simple Add", "x = 2+2", "x", "4" );
          viewmodel.UnitTests.Add( defaultTest );

          var l = new L( _browser );
          var versionTest = new JsUnitTest( _browser, "Check Leaflet Version", "version = L.version", "version", "0.7.7" );
          viewmodel.UnitTests.Add( versionTest );

          // todo make these into tests also
          var map = await l.Map( "map", "map" );
          await l.Evaluate( "map.setView([51.505, -0.09], 13);" );

          var url = @"https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw";
          
          var tileLayer = await l.TileLayer( "mapboxTileLayer", url, new TileLayerOptions { id = "mapbox.streets" } );
          var mapTest = new JsUnitTest( _browser, "Check TileLayer Options", "2+2", $"{tileLayer.JsName}.options", tileLayer.Options.ToString() );
          viewmodel.UnitTests.Add( mapTest );

          var tileOptions = new  TileLayerOptions { id = "mapbox.streets" };
          var action = new LeafnetActionUnitTest<TileLayer, TileLayerOptions>( _browser,
            () => l.TileLayer( "mapboxTileLayer", url, tileOptions ),
            layer => $"{layer.JsName}.options",
            options => options.id == tileOptions.id
            );

          await action.RunTest();
        } );
      };

      Closing += delegate
      {
        _browser.Dispose();
      };
    }
  }
}
