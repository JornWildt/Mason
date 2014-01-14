using Mason.IssueTracker.Server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mason.IssueTracker.Server.Domain
{
  public static class ErrorHandling
  {
    public class Codes
    {
      public const string InputValidation = "INVALIDINPUT";
      public const string MissingResource = "NOTFOUND";
      public const string InternalError = "INTERNALERROR";
    }


    public static void ValidateInput(params Action[] actions)
    {
      List<string> messages = null;

      foreach (Action a in actions)
      {
        try
        {
          a();
        }
        catch (Exception ex)
        {
          if (messages == null)
            messages = new List<string>();
          messages.Add(ex.Message);
        }
      }

      if (messages != null)
        throw new DomainException("There was a problem with one or more input values.", Codes.InputValidation, messages);
    }
  }
}
