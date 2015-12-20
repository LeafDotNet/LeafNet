using System.Threading.Tasks;
using CefSharp;
using System;

namespace Leafnet.Wpf
{
  public partial class MainWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      browser.Address = "custom://web/index.html";
      browser.IsBrowserInitializedChanged += OnBrowserInitializedChanged;
    }

    private void OnBrowserInitializedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
      if (browser.IsBrowserInitialized)
      {
        LoadMap();
        //browser.ShowDevTools();
      }
    }

    public async void LoadMap()
    {
      var s2 = "alert(document.body.style.backgroundColor)";
      //var script = "L.map('map').setView([51.505, -0.09], 13);";
      browser.ExecuteScriptAsync(s2);
    }
  }
}
