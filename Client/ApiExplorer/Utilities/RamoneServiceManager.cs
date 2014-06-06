using ApiExplorer.MediaTypeHandlers.ApplicationMason;
using Ramone;


namespace ApiExplorer.Utilities
{
  public static class RamoneServiceManager
  {
    private static IService Service { get; set; }

    public static ISession Session { get; private set; }


    static RamoneServiceManager()
    {
      Service = RamoneConfiguration.NewService();
      Service.UserAgent = "API Explorer";
      Service.CodecManager.AddCodec<JsonNetCodec>(new MediaType("application/vnd.mason+json"));

      Session = Service.NewSession();
    }
  }
}
