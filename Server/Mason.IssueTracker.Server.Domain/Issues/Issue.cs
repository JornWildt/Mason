using CuttingEdge.Conditions;
using System;


namespace Mason.IssueTracker.Server.Domain.Issues
{
  public class Issue
  {
    public long Id { get; internal set; }

    public string Title { get; protected set; }

    public string Description { get; protected set; }


    public Issue(string title, string description)
    {
      Condition.Requires(title, "title").IsNotNullOrWhiteSpace();
      Condition.Requires(description, "description").IsNotNull();

      Title = title;
      Description = description;
    }
  }
}
