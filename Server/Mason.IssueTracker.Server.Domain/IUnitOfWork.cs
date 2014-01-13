using System;


namespace Mason.IssueTracker.Server.Domain
{
  public interface IUnitOfWork : IDisposable
  {
    void Commit();
    void Rollback();
  }
}
