using Ramone;


namespace ApiExplorer.Utilities
{
  public class MethodOverrideInterceptor : IRequestInterceptor2
  {
    public void DataSent(RequestContext context)
    {
    }

    public void HeadersReady(RequestContext context)
    {
    }

    public void MethodSet(RequestContext context)
    {
      if (context.Request.Method != "GET" && context.Request.Method != "POST")
      {
        context.Request.Headers["X-HTTP-Method-Override"] = context.Request.Method;
        context.Request.Method = "POST";
      }
    }
  }
}
