using Mason.IssueTracker.Server.Domain.Exceptions;
using NHibernate.Exceptions;
using System;
using System.Data.SQLite;


namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public static class ExceptionConverter
  {
    public static Exception ConvertException(Exception ex)
    {
      if (ex is GenericADOException && ex.InnerException != null)
        return ConvertException(ex.InnerException);

      if (ex is SQLiteException)
      {
        SQLiteException sqex = (SQLiteException)ex;
        if (sqex.ErrorCode == 19)
          return new DuplicateKeyException(sqex.Message);
      }

      return ex;
    }
  }
}
