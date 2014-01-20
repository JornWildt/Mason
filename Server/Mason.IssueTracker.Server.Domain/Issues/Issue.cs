using CuttingEdge.Conditions;
using Mason.IssueTracker.Server.Domain.Comments;
using Mason.IssueTracker.Server.Domain.Projects;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Issues
{
  public class Issue
  {
    public virtual int Id { get; protected set; }

    public virtual Project OwnerProject { get; protected set; }

    public virtual string Title { get; protected set; }

    public virtual string Description { get; protected set; }

    public virtual int Severity { get; protected set; }

    public virtual DateTime CreatedDate { get; protected set; }


    public Issue()
    {
    }


    public Issue(Project owner, string title, string description, int severity)
    {
      Condition.Requires(owner, "owner").IsNotNull();
      OwnerProject = owner;
      Update(title, description, severity);
      CreatedDate = DateTime.Now;
    }


    public virtual void Update(string title, string description, int severity)
    {
      ErrorHandling.ValidateInput(
        () => Condition.Requires(title, "title").IsNotNullOrWhiteSpace().IsNotLongerThan(255),
        () => Condition.Requires(description, "description").IsNotNull(),
        () => Condition.Requires(severity).IsInRange(1,5));

      Title = title;
      Description = description;
      Severity = severity;
    }
  }
}
