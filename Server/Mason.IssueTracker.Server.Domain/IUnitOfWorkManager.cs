using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mason.IssueTracker.Server.Domain
{
  public interface IUnitOfWorkManager
  {
    IUnitOfWork CreateUnitOfWork();
  }
}
