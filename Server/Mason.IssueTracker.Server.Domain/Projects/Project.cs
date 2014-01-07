using CuttingEdge.Conditions;
using System;


namespace Mason.IssueTracker.Server.Domain.Projects
{
  public class Project
  {
    public Guid Id { get; private set; }

    public string Abbreviation { get; protected set; }

    public string Title { get; protected set; }

    public string Description { get; protected set; }


    public Project(string abbreviation, string title, string description)
    {
      Condition.Requires(abbreviation, "abbreviation").IsNotNullOrWhiteSpace();
      Condition.Requires(title, "title").IsNotNullOrWhiteSpace();
      Condition.Requires(description, "description").IsNotNull();
      
      Id = Guid.NewGuid();
      Title = title;
      Description = description;
    }
  }
}
