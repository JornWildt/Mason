using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public class NHibernateUnitOfWorkManager : IUnitOfWorkManager
  {
    #region IUnitOfWorkManager Members

    public IUnitOfWork CreateUnitOfWork()
    {
      return new NHibernateUnitOfWork(SessionManager.CurrentSession);
    }

    #endregion
  }
}
