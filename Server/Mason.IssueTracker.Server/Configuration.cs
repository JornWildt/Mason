using log4net;
using Mason.IssueTracker.Server.Domain;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.NHibernate;
using Mason.IssueTracker.Server.Domain.Projects;
using OpenRasta.Configuration;
using OpenRasta.Web.UriDecorators;
using System;


namespace Mason.IssueTracker.Server
{
  public class Configuration : IConfigurationSource
  {
    static ILog Logger = LogManager.GetLogger(typeof(Configuration));


    public void Configure()
    {
      InitializeLogging();

      try
      {
        ResourceSpace.Uses.CustomDependency<IUnitOfWorkManager, NHibernateUnitOfWorkManager>(OpenRasta.DI.DependencyLifetime.Singleton);

        // Initialize OpenRasta/modules
        using (OpenRastaConfiguration.Manual)
        {
          ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
          Projects.ApplicationStarter.Start();
          Issues.ApplicationStarter.Start();
          Contact.ApplicationStarter.Start();
          ResourceCommons.ApplicationStarter.Start();
          ServiceIndex.ApplicationStarter.Start();
        }

        // Setup default data
        SessionManager.ExecuteUnitOfWork(() =>
        {
          IIssueRepository issueRepository = (IIssueRepository)ResourceSpace.Uses.Resolver.Resolve(typeof(IIssueRepository));
          IProjectRepository projectRepository = (IProjectRepository)ResourceSpace.Uses.Resolver.Resolve(typeof(IProjectRepository));
          DemoDataGenerator.GenerateDemoData(issueRepository, projectRepository);
        });
      }
      catch (Exception ex)
      {
        Logger.Fatal(ex);
        throw;
      }
    }

    
    private void InitializeLogging()
    {
      log4net.Config.XmlConfigurator.Configure();
      Logger.Info("***********************************************************************");
      Logger.Info("Starting IssueTracker server");
      Logger.Info("***********************************************************************");
    }
  }
}
