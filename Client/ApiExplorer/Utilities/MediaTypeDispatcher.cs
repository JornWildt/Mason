using ApiExplorer.MediaTypeHandlers.ApplicationMason;
using ApiExplorer.MediaTypeHandlers.Text;
using ApiExplorer.MediaTypeHandlers.Unknown;
using Ramone;
using System.Collections.Generic;
using ApiExplorer.MediaTypeHandlers.Image;


namespace ApiExplorer.Utilities
{
  public static class MediaTypeDispatcher
  {
    private static Dictionary<string, IHandleMediaType> MediaTypeHandlers = new Dictionary<string, IHandleMediaType>();

    private static UnknownMediaTypeHandler NoContentMediaTypeHandler { get; set; }
    private static UnknownMediaTypeHandler UnknownMediaTypeHandler { get; set; }


    static MediaTypeDispatcher()
    {
      UnknownMediaTypeHandler = new UnknownMediaTypeHandler("Unknown media type");
      NoContentMediaTypeHandler = new UnknownMediaTypeHandler("No content");
      RegisterHandler("application/vnd.mason", new ApplicationMasonMediaTypeHandler());
      RegisterHandler("text/plain", new TextMediaTypeHandler());
      RegisterHandler("text/vcard", new TextMediaTypeHandler());
      RegisterHandler("application/json", new TextMediaTypeHandler());
      RegisterHandler("application/jcard+json", new TextMediaTypeHandler());
      RegisterHandler("application/schema+json", new TextMediaTypeHandler());
      RegisterHandler("image/png", new ImageMediaTypeHandler());
    }


    public static void RegisterHandler(string mediaType, IHandleMediaType handler)
    {
      MediaTypeHandlers[mediaType] = handler;
    }


    public static IHandleMediaType GetMediaTypeHandler(Response r)
    {
      if (r.ContentType != null)
      {
        string mediaType = r.ContentType.ToString();
        if (MediaTypeHandlers.ContainsKey(mediaType))
        {
          IHandleMediaType handler = MediaTypeHandlers[mediaType];
          return handler;
        }
        else
          return UnknownMediaTypeHandler;
      }

      return NoContentMediaTypeHandler;
    }
  }
}
