using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Exceptions
{
  public class DomainException : Exception
  {
    public List<string> Messages { get; set; }

    public string Details { get; set; }


    public DomainException(string message, IEnumerable<string> messages = null, string details = null)
      : base(message)
    {
      Messages = new List<string>(messages);
      Details = details;
    }
  }
}
