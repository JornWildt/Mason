using log4net;
using NHibernate;
using System;


namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public class NHibernateUnitOfWork : IUnitOfWork
  {
    static ILog Logger = LogManager.GetLogger(typeof(NHibernateUnitOfWork));


    protected ISession Session { get; private set; }

    protected ITransaction Transaction { get; private set; }


    public NHibernateUnitOfWork(ISession session)
    {
      Session = session;
      Transaction = session.BeginTransaction();
    }

    
    #region IUnitOfWork Members

    public void Commit()
    {
      if (Transaction != null)
        Transaction.Commit();
    }

    
    public void Rollback()
    {
      if (Transaction != null)
        Transaction.Rollback();
    }

    #endregion


    #region IDisposable Members

    public void Dispose()
    {
      try
      {
        Transaction.Dispose();
      }
      catch (Exception ex)
      {
        Logger.Debug("Failed to dispose transaction", ex);
        throw;
      }
      finally
      {
        try
        {
          Session.Dispose();
        }
        catch (Exception ex)
        {
          Logger.Debug("Failed to dispose session", ex);
          throw;
        }
      }
    }

    #endregion
  }
}
