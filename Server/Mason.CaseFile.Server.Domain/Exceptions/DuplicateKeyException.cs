using System;


namespace Mason.CaseFile.Server.Domain.Exceptions
{
  public class DuplicateKeyException : Exception
  {
    public DuplicateKeyException(string msg, params object[] p)
      : base(string.Format(msg, p))
    {
    }
  }
}
