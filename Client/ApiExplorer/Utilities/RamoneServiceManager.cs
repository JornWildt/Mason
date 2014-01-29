using ApiExplorer.MediaTypeHandlers.ApplicationMason;
using Ramone;


namespace ApiExplorer.Utilities
{
  public static class RamoneServiceManager
  {
    public static IService Service { get; private set; }


    static RamoneServiceManager()
    {
      Service = RamoneConfiguration.NewService();
      Service.UserAgent = "API Explorer";
      Service.CodecManager.AddCodec<JsonNetCodec>(new MediaType("application/vnd.mason"));
    }
  }
}
