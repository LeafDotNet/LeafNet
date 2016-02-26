using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Leafnet.Wpf.Tests.MapTest;
using Leafnet.Wpf.Tests.MapTest.UnitTests;
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

      var viewmodel = new JsUnitTestsViewModel();
      _jsunitTests.DataContext = viewmodel;

      _browser.FrameLoadEnd += delegate
      {
        Dispatcher.InvokeAsync( async () =>
        {
          var l = new L( _browser );

          // todo make these into tests also
          var map = await l.Map( "map", "map" );
          await l.Evaluate( "map.setView([51.505, -0.09], 13);" );

          var url = @"https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw";
          
          var tileOptions = new  TileLayerOptions { id = "mapbox.streets" };
          var tileLayer = new TileLayer( "mapboxTileLayer", _browser, url, tileOptions );

          var createTileLayerTest = new JsFunctionTest<TileLayer, TileLayerOptions>( 
            browser: _browser,
            description: "Creating Tile Layer",   
            execution: () => l.TileLayer(tileLayer),
            evalScript: layer => $"{layer.JsName}.options",
            expectedResult: tileOptions
            );

          //          var addTileToMapTest = new JsFunctionTest<TileLayer, bool>(
          //            browser: _browser,
          //            description: "Add Tile To Map",
          //            execution: () => tileLayer.AddToMap(map),
          //            evalScript: _ => $"{map.JsName}.hasLayer({tileLayer.JsName});",
          //            expectedResult: true
          //            );

          //          var checkMapTest = new LeafnetUnitTest<TileLayer,bool>(
          //            description: "map.hasLayer(tilelayer);",
          //            execScript: () => tileLayer.AddToMap( map ),
          //            evalScript: () => map.HasLayer(tileLayer),
          //            equalsCheck: value => value == true
          //          );

          var addToMapTest = new LeafnetBasicUnitTest<TileLayer>(
            description: "tileLayer.AddToMap( map );",
            execScript: () => tileLayer.AddToMap( map )
          );

          var checkMapTest = new LeafnetBasicUnitTest<bool>(
            description: "map.HasLayer(tileLayer);",
            execScript: () => map.HasLayer(tileLayer),
            equalsCheck: value => value
          );

//          var removeLayerTest = new LeafnetBasicUnitTest<Map>(
//            description: "Removing Tile Layer",
//            execScript: () => map.RemoveLayer( tileLayer )
//          );
//
//          var checkLayerRemoved = new LeafnetBasicUnitTest<bool>(
//            description: "Removed Tile Layer",
//            execScript: () => map.HasLayer( tileLayer ),
//            equalsCheck: value => value == false
//          );

          var removeLayer = new LeafnetUnitTest<Map,bool>(
            description: "Removing Map Layer",
            execScript: () => map.RemoveLayer(tileLayer),
            evalScript: () => map.HasLayer(tileLayer),
            equalsCheck: value => value == false
          );

          var removeLayer2 = new LeafnetUnitTest<Map, bool>(
            description: "Removing Map Layer",
            execScript: () => map.RemoveLayer( tileLayer ),
            evalScript: () => map.HasLayer( tileLayer ),
            equalsCheck: value => value == false
          );




          viewmodel.UnitTests.Add( createTileLayerTest );
          viewmodel.UnitTests.Add( addToMapTest );
          viewmodel.UnitTests.Add( checkMapTest );
//          viewmodel.UnitTests.Add( removeLayerTest );
//          viewmodel.UnitTests.Add( checkLayerRemoved );
          viewmodel.UnitTests.Add( removeLayer );
          viewmodel.UnitTests.Add( removeLayer2 );
        } );
      };

      Closing += delegate
      {
        _browser.Dispose();
      };
    }
  }
}
