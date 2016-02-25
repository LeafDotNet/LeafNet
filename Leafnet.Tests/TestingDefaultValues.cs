using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Leafnet.Tests
{
  public class TestingDefaultValues
  {
    public class TileOptionsMock
    {
      public TileOptionsMock()
      {
        Zooms = new int[] {};
      }

      [DefaultValue( new int[] {10} )]
      public int[] Zooms { get; set; }
    }

    [Fact]
    public void ClassEquals()
    {
      var t1 = new TileLayerOptions();
      var t2 = new TileLayerOptions();
      Assert.Equal( t1, t2 );
    }

    [Fact]
    public void TestTileLayerOptionsDefaultValue()
    {
      // currently jsonconvert does not respect array as default values
      var expected = "{\"subdomains\":[\"a\",\"b\",\"c\"]}";
      var tileLayer = new TileLayerOptions();
      var actual = tileLayer.ToString();
      Assert.Equal( expected, actual );
    }

    [Fact]
    public void GetDefaultArray()
    {
      var tileOptions = new TileOptionsMock();
      var attributes = TypeDescriptor.GetProperties( tileOptions )["Zooms"].Attributes;
      DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)attributes[typeof( DefaultValueAttribute )];
      var expected = new int[] { 10 };
      var actual = (int[]) defaultValueAttribute.Value;
      Assert.Equal( expected, actual );
    }
  }
}
