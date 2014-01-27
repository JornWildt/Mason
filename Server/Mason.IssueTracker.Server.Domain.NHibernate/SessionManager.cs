using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Mason.IssueTracker.Server.Domain.NHibernate.Projects;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.IO;
using log4net;
using System.Configuration;


namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public static class SessionManager
  {
    static ILog Logger = LogManager.GetLogger(typeof(SessionManager));


    public static void ExecuteUnitOfWork(Action a)
    {
      try
      {
        using (var transaction = CurrentSession.BeginTransaction())
        {
          a();
          transaction.Commit();
        }
      }
      finally
      {
        DisposeCurrentSession();
      }
    }


    public static void Restart()
    {
    }


    private static ISessionFactory _sessionFactory;
    public static ISessionFactory SessionFactory
    {
      get
      {
        if (_sessionFactory == null)
          _sessionFactory = CreateSessionFactory();
        return _sessionFactory;
      }
    }


    private static ISessionFactory CreateSessionFactory()
    {
      Logger.DebugFormat("DataDirectory: {0}", AppDomain.CurrentDomain.GetData("DataDirectory"));

      // delete the existing db on each run
      string dbname = ConfigurationManager.AppSettings["Database.Filename"];
      Logger.DebugFormat("Try delete: {0}", dbname);
      if (dbname != null && File.Exists(dbname))
        File.Delete(dbname);

      var cfg = new global::NHibernate.Cfg.Configuration();
      cfg.Configure();

      return Fluently.Configure(cfg)
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProjectMapping>())
        .ExposeConfiguration(BuildSchema)
        .BuildSessionFactory();
    }


    private static void BuildSchema(global::NHibernate.Cfg.Configuration config)
    {
      config.SetProperty(global::NHibernate.Cfg.Environment.CurrentSessionContextClass, "web");

      // this NHibernate tool takes a configuration (with mapping info in)
      // and exports a database schema from it
      new SchemaExport(config).Create(false, true);
    }



    public static ISession CurrentSession
    {
      get
      {
        if (!CurrentSessionContext.HasBind(SessionFactory))
          CurrentSessionContext.Bind(SessionFactory.OpenSession());

        return SessionFactory.GetCurrentSession();
      }
    }


    public static void DisposeCurrentSession()
    {
      ISession currentSession = CurrentSessionContext.Unbind(SessionFactory);

      currentSession.Close();
      currentSession.Dispose();
    }
  }
}
