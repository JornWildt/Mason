using System;
using System.Threading;
using System.Web;
using log4net;


namespace Mason.IssueTracker.Server.Utility
{
  public static class ApplicationLifeTimeManager
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationLifeTimeManager));

    // Seconds
    static readonly int ApplicationLifeTime = 10 * 60 * 1000;

    public static DateTime NextRestart { get; set; }

    private static Timer RestartTimer { get; set; }


    static ApplicationLifeTimeManager()
    {
      NextRestart = DateTime.Now;
    }


    public static void Start()
    {
      RestartTimer = new Timer(RestartApplication, null, ApplicationLifeTime, 0);
      NextRestart = DateTime.Now + TimeSpan.FromMilliseconds(ApplicationLifeTime);
      Logger.DebugFormat("Next restart: {0}", NextRestart);
    }


    private static void RestartApplication(object state)
    {
      Logger.Debug("### RESTART APPLICATION ###");
      HttpRuntime.UnloadAppDomain();
    }
  }
}
