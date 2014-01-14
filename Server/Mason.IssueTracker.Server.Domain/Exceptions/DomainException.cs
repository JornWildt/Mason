using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Exceptions
{
  public class DomainException : Exception
  {
    public string Code { get; set; }

    public List<string> Messages { get; set; }

    public string Details { get; set; }


    public DomainException(string message, string code, IEnumerable<string> messages = null, string details = null)
      : base(message)
    {
      Code = code;
      Messages = new List<string>(messages);
      Details = details;
    }
  }
}
