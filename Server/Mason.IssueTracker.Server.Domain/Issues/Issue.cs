using CuttingEdge.Conditions;
using Mason.IssueTracker.Server.Domain.Comments;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Issues
{
  public class Issue
  {
    public long Id { get; internal set; }

    public string Title { get; protected set; }

    public string Description { get; protected set; }

    public int Severity { get; protected set; }

    public DateTime CreatedDate { get; protected set; }

    public IEnumerable<Comment> Comments { get { return CommentList.AsReadOnly(); } }


    protected List<Comment> CommentList { get; set; }


    public Issue(string title, string description, int severity)
    {
      Condition.Requires(title, "title").IsNotNullOrWhiteSpace();
      Condition.Requires(description, "description").IsNotNull();
      Condition.Requires(severity, "severity").IsInRange(1, 5);

      Title = title;
      Description = description;
      Severity = severity;
      CreatedDate = DateTime.Now;

      CommentList = new List<Comment>();
    }


    public void AddComment(Comment c)
    {
      Condition.Requires(c, "c").IsNotNull();
      CommentList.Add(c);
    }
  }
}
