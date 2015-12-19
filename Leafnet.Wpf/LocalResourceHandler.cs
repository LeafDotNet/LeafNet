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
      // The 'host' portion is entirely ignored by this scheme handler.
      var uri = new Uri(request.Url);
      var fileName = uri.AbsolutePath;

      Task.Run(() =>
        {
          using (callback)
          {
            var bytes = Encoding.UTF8.GetBytes(fileName);
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
