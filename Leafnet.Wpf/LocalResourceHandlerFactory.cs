﻿using CefSharp;

namespace Leafnet.Wpf
{
  internal class LocalSchemeHandlerFactory : ISchemeHandlerFactory
  {
    public const string SchemeName = "custom";

    public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
    {
      return new LocalResourceHandler();
    }
  }
}