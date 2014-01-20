using log4net;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.NHibernate;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.Net;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.OperationModel.Interceptors;
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
        ResourceSpace.Uses.CustomDependency<IUnitOfWorkManager, NHibernateUnitOfWorkManager>(DependencyLifetime.Singleton);

        // Initialize OpenRasta/modules
        using (OpenRastaConfiguration.Manual)
        {
          ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
          ResourceSpace.Uses.CustomDependency<IOperationInterceptor, OperationInterceptor>(DependencyLifetime.PerRequest);
          Projects.ApplicationStarter.Start();
          Issues.ApplicationStarter.Start();
          Contact.ApplicationStarter.Start();
          ResourceCommons.ApplicationStarter.Start();
          JsonSchemas.ApplicationStarter.Start();

          ResourceSpace.Has.ResourcesOfType<Resource>()
                       .WithoutUri
                       .TranscodedBy<IssueTrackerMasonCodec>();
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
