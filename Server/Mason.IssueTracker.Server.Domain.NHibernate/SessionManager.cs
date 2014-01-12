using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Mason.IssueTracker.Server.Domain.NHibernate.Projects.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public static class SessionManager
  {
    private const string DBFilename = "C:\\temp\\IssueTracker.db";


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
      // delete the existing db on each run
      if (File.Exists(DBFilename))
        File.Delete(DBFilename);

      return Fluently.Configure()
        .Database(
          SQLiteConfiguration.Standard
            .UsingFile(DBFilename)
        )
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProjectMapping>())
        .ExposeConfiguration(BuildSchema)
        .BuildSessionFactory();
    }


    private static void BuildSchema(Configuration config)
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
