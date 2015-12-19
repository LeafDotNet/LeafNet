using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Leafnet.Wpf
{
  public class LocalResourceHandler : IResourceHandler
  {
    private string mimeType;
    private MemoryStream stream;

    public bool ProcessRequestAsync(IRequest request, ICallback callback)
    {
      var uri = new Uri(request.Url);
      var fileName = request.Url.TrimStart(LocalSchemeHandlerFactory.SchemeName + "://");

      Task.Run(() =>
        {
          using (callback)
          {
            var fileAsString = File.ReadAllText(fileName);

            var bytes = Encoding.UTF8.GetBytes(fileAsString);
            stream = new MemoryStream(bytes);

            var fileExtension = Path.GetExtension(fileName);
            mimeType = ResourceHandler.GetMimeType(fileExtension);

            callback.Continue();
          }
        });

        return true;
      }

    public Stream GetResponse(IResponse response, out long responseLength, out string redirectUrl)
    {
      responseLength = stream.Length;
      redirectUrl = null;

      response.StatusCode = (int)HttpStatusCode.OK;
      response.StatusText = "OK";
      response.MimeType = mimeType;

      return stream;
    }
  }
}
