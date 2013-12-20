using System.Diagnostics;
using log4net;


namespace Mason.CaseFile.Server.Utility
{
  public class Log4NetTraceListener : TraceListener
  {
    public ILog Logger { get; set; }

    public Log4NetTraceListener()
    {
      Logger = log4net.LogManager.GetLogger(typeof(Log4NetTraceListener));
    }

    public override void Write(string value)
    {
      Logger.Debug(value);
    }

    public override void WriteLine(string value)
    {
      Logger.Debug(value);
    }
  }
}
