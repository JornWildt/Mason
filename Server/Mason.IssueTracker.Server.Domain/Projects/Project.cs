using CuttingEdge.Conditions;
using System;


namespace Mason.IssueTracker.Server.Domain.Projects
{
  public class Project
  {
    public virtual int Id { get; protected set; }

    public virtual string Code { get; protected set; }

    public virtual string Title { get; protected set; }

    public virtual string Description { get; protected set; }


    public Project()
    {
    }


    public Project(string code, string title, string description)
    {
      Condition.Requires(code, "code").IsNotNullOrWhiteSpace();
      Condition.Requires(title, "title").IsNotNullOrWhiteSpace();
      Condition.Requires(description, "description").IsNotNull();
      
      Code = code;
      Title = title;
      Description = description;
    }
  }
}
