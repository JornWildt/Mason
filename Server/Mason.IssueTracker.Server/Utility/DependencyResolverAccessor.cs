using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mason.IssueTracker.Server.Domain.Issues;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using System;


namespace Mason.IssueTracker.Server.Utility
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
