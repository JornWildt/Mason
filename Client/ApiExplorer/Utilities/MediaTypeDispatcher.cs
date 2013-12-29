using ApiExplorer.MediaTypeHandlers.ApplicationMason;
using ApiExplorer.MediaTypeHandlers.Unknown;
using Ramone;
using System.Collections.Generic;


namespace ApiExplorer.Utilities
{
  public static class MediaTypeDispatcher
  {
    private static Dictionary<string, IHandleMediaType> MediaTypeHandlers = new Dictionary<string, IHandleMediaType>();

    public const string UnknownMediaTypeId = "*UNKNOWN*";


    static MediaTypeDispatcher()
    {
      RegisterHandler(UnknownMediaTypeId, new UnknownMediaTypeHandler());
      RegisterHandler("application/vnd.mason", new ApplicationMasonMediaTypeHandler());
    }


    public static void RegisterHandler(string mediaType, IHandleMediaType handler)
    {
      MediaTypeHandlers[mediaType] = handler;
    }


    public static IHandleMediaType GetMediaTypeHandler(Response r)
    {
      string mediaType = r.ContentType.ToString();
      if (!MediaTypeHandlers.ContainsKey(mediaType))
        mediaType = UnknownMediaTypeId;

      IHandleMediaType handler = MediaTypeHandlers[mediaType];
      return handler;
    }
  }
}
