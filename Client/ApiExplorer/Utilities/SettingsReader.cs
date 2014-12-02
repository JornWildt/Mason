using log4net;
using System;


namespace ApiExplorer.Utilities
{
  public static class SettingsReader
  {
    static ILog Logger = LogManager.GetLogger(typeof(SettingsReader));


    public static void Set(Action f)
    {
      try
      {
        f();
      }
      catch (Exception ex)
      {
        Logger.Error(ex);
      }
    }


    public static T Get<T>(Func<T> f, T errorValue)
    {
      try
      {
        return f();
      }
      catch (Exception ex)
      {
        Logger.Error(ex);
        return errorValue;
      }
    }
  }
}
