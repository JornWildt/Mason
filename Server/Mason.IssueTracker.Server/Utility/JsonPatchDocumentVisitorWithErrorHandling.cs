using log4net;
using Ramone.MediaTypes.JsonPatch;
using System;


namespace Mason.IssueTracker.Server.Utility
{
  public class JsonPatchDocumentVisitorWithErrorHandling<T> : JsonPatchDocumentVisitor<T>
  {
    protected ILog Logger = LogManager.GetLogger(typeof(JsonPatchDocumentVisitorWithErrorHandling<T>));


    public override bool Replace(string path, object value)
    {
      return Exec(string.Format("Replace({0},{1})", path, (value != null ? value.ToString() : "null")),
        () => SafeReplace(path, value));
    }

    public virtual bool SafeReplace(string path, object value)
    {
      return base.Replace(path, value);
    }


    public override bool Copy(string from, string path)
    {
      return Exec(string.Format("Copy({0},{1})", from, path),
        () => SafeCopy(from, path));
    }

    public virtual bool SafeCopy(string from, string path)
    {
      return base.Copy(from, path);
    }


    public override bool Move(string from, string path)
    {
      return Exec(string.Format("Move({0},{1})", from, path),
        () => SafeMove(from, path));
    }

    public virtual bool SafeMove(string from, string path)
    {
      return base.Move(from, path);
    }


    public override bool Add(string path, object value)
    {
      return Exec(string.Format("Add({0},{1})", path, (value != null ? value.ToString() : "null")),
        () => SafeAdd(path, value));
    }

    public virtual bool SafeAdd(string path, object value)
    {
      return base.Add(path, value);
    }


    public override bool Remove(string path)
    {
      return Exec(string.Format("Remove({0})", path),
        () => SafeRemove(path));
    }

    public virtual bool SafeRemove(string path)
    {
      return base.Remove(path);
    }


    public override bool Test(string path, object value)
    {
      return Exec(string.Format("Test({0},{1})", path, (value != null ? value.ToString() : "null")),
        () => SafeTest(path, value));
    }

    public virtual bool SafeTest(string path, object value)
    {
      return base.Test(path, value);
    }


    protected bool Exec(string operationInfo, Func<bool> f)
    {
      try
      {
        return f();
      }
      catch (Exception ex)
      {
        Logger.Info(string.Format("Exception in JSON patch visitor for {0}.", operationInfo), ex);
        throw;
      }
    }
  }
}
