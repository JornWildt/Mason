using System;


namespace Mason.IssueTracker.Server.Domain.Exceptions
{
  public class DuplicateKeyException : Exception
  {
    public DuplicateKeyException(string msg)
      : base(msg)
    {
    }
  }
}
