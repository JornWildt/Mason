using log4net;
using Mason.IssueTracker.Server.Domain;
using System;


namespace Mason.IssueTracker.Server
{
  public class BaseHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(BaseHandler));


    #region Dependencies

    public IUnitOfWorkManager UnitOfWorkManager { get; set; }

    #endregion


    protected T ExecuteInUnitOfWork<T>(Func<T> f)
    {
      using (IUnitOfWork uow = NewUnitOfWork())
      {
        try
        {
          T result = f();
          uow.Commit();
          return result;
        }
        catch (Exception)
        {
          uow.Rollback();
          throw;
        }
      }
    }


    protected IUnitOfWork NewUnitOfWork()
    {
      return UnitOfWorkManager.CreateUnitOfWork();
    }
  }
}
