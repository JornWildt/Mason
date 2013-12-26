using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mason.CaseFile.Server.Domain.CaseFiles;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using System;


namespace Mason.CaseFile.Server.Utility
{
  public class DependencyResolverAccessor //: IDependencyResolverAccessor
  {
    static IWindsorContainer container;
    public static IWindsorContainer WindsorContainer
    {
      get
      {
        if (container == null)
          container = ConfigureContainer();
        return container;
      }           
    }

    
    static IWindsorContainer ConfigureContainer()
    {
      container = new WindsorContainer();

      container.Register(Component.For<ICaseFileRepository>().ImplementedBy<CaseFileInMemoryRepository>());

      return container;
    }

    
    #region IDependencyResolverAccessor Members

    public IDependencyResolver Resolver
    {
      get 
      {
        return new WindsorDependencyResolver(WindsorContainer);
      }
    }

    #endregion
  }
}
