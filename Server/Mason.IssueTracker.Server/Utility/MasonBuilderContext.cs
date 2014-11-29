using MasonBuilder.Net;
using OpenRasta.Web;


namespace Mason.IssueTracker.Server.Utility
{
  public class MasonBuilderContext : IMasonBuilderContext
  {
    #region IMasonBuilderContext Members

    public bool PreferMinimalResponse
    {
      get
      {
        return CommunicationContext.Request.Headers.ContainsKey("Prefer") && CommunicationContext.Request.Headers["Prefer"].Contains("return=minimal");
      }
    }

    #endregion


    public ICommunicationContext CommunicationContext { get; set; }


    public MasonBuilderContext(ICommunicationContext context)
    {
      CommunicationContext = context;
    }
  }
}
