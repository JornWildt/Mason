using System;
using System.Threading;
using System.Web;
using log4net;
using System.Configuration;


namespace Mason.IssueTracker.Server.Utility
{
  public static class ApplicationLifeTimeManager
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationLifeTimeManager));

    // Seconds
    public static int ApplicationLifeTime
    {
      get
      {
        string ls = ConfigurationManager.AppSettings["Application.Lifetime"];
        int minutes;
        if (ls != null && int.TryParse(ls, out minutes))
          return minutes * 60 * 1000;
        return 10 * 60 * 1000;
      }
    }

    private static Timer RestartTimer { get; set; }


    public static DateTime NextRestart { get; set; }


    static ApplicationLifeTimeManager()
    {
      NextRestart = DateTime.Now;
    }


    public static void Start()
    {
      RestartTimer = new Timer(RestartApplication, null, ApplicationLifeTime, ApplicationLifeTime+5000);
      NextRestart = DateTime.Now + TimeSpan.FromMilliseconds(ApplicationLifeTime);
      Logger.DebugFormat("Next restart: {0}", NextRestart);
    }


    private static void RestartApplication(object state)
    {
      Logger.Debug("### Test application restart ###");
      if (DateTime.Now > NextRestart)
      {
        Logger.Debug("### RESTART APPLICATION ###");
        HttpRuntime.UnloadAppDomain();
      }
    }
  }
}
