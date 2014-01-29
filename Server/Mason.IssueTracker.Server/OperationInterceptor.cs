using log4net;
using Mason.IssueTracker.Server.Domain;
using Mason.IssueTracker.Server.Domain.Exceptions;
using Mason.Net;
using OpenRasta.OperationModel;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Mason.IssueTracker.Server
{
  public class OperationInterceptor : IOperationInterceptor
  {
    static ILog Logger = LogManager.GetLogger(typeof(OperationInterceptor));

    public ICommunicationContext CommunicationContext { get; set; }


    #region IOperationInterceptor Members

    public bool AfterExecute(IOperation operation, IEnumerable<OutputMember> outputMembers)
    {
      return true;
    }


    public bool BeforeExecute(OpenRasta.OperationModel.IOperation operation)
    {
      string method = CommunicationContext.Request.HttpMethod;
      string url = CommunicationContext.Request.Uri.AbsoluteUri;
      string userAgent = CommunicationContext.Request.Headers["User-Agent"];
      Logger.DebugFormat("Incoming {0} request for {1} [{2}]", method, url, userAgent);
      return true;
    }


    public Func<IEnumerable<OutputMember>> RewriteOperation(Func<IEnumerable<OutputMember>> operationBuilder)
    {
      return () =>
      {
        try
        {
          return operationBuilder();
        }
        catch (Exception ex)
        {
          return BuildOutputFromException(ex);
        }
      };
    }

    #endregion


    private OutputMember[] BuildOutputFromException(Exception ex)
    {
      if (ex is TargetInvocationException)
        return BuildOutputFromException(ex.InnerException);

      ex = Mason.IssueTracker.Server.Domain.NHibernate.ExceptionConverter.ConvertException(ex);

      string id = Guid.NewGuid().ToString();

      SubResource error = new SubResource();
      error[MasonProperties.ErrorProperties.Id] = id;
      error[MasonProperties.ErrorProperties.Message] = ex.Message;

      Resource result = new Resource();
      result[MasonProperties.Error] = error;
      result.SetMeta(MasonProperties.MetaProperties.Title, "Application error");
      result.SetMeta(MasonProperties.MetaProperties.Description, "Something went wrong while processing the request. The data contained in this response should explain the problem.");

      if (ex is DomainException)
      {
        Logger.Info(string.Format("{0} [{1}]", ex.Message, id), ex);
        
        DomainException dex = (DomainException)ex;
        error[MasonProperties.ErrorProperties.Code] = dex.Code;

        if (dex.Messages != null && dex.Messages.Count > 0)
          error[MasonProperties.ErrorProperties.Messages] = new List<string>(dex.Messages);

        return new[] 
        {
          new OutputMember { Value = new OperationResult.BadRequest { ResponseResource = result } }
        };
      }
      else if (ex is MissingResourceException)
      {
        Logger.Info(string.Format("{0} [{1}]", ex.Message, id), ex);

        error[MasonProperties.ErrorProperties.Code] = ErrorHandling.Codes.MissingResource; ;

        return new[] 
        {
          new OutputMember { Value = new OperationResult.NotFound { ResponseResource = result } }
        };
      }
      else if (ex is DuplicateKeyException)
      {
        Logger.Info(string.Format("{0} [{1}]", ex.Message, id), ex);

        DuplicateKeyException dkex = (DuplicateKeyException)ex;
        error[MasonProperties.ErrorProperties.Code] = ErrorHandling.Codes.DuplicateKey;

        return new[] 
        {
          new OutputMember { Value = new OperationResult.BadRequest { ResponseResource = result } }
        };
      }
      else
      {
        Logger.Error(string.Format("{0} [{1}]", ex.Message, id), ex);

        error[MasonProperties.ErrorProperties.Code] = ErrorHandling.Codes.InternalError; ;

        return new[] 
        {
          new OutputMember { Value = new OperationResult.InternalServerError { ResponseResource = result } }
        };
      }
    }
  }
}
