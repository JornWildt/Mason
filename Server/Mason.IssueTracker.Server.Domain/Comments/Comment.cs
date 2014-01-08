using CuttingEdge.Conditions;
using System;


namespace Mason.IssueTracker.Server.Domain.Comments
{
  public class Comment
  {
    public long Id { get; internal set; }

    public string Text { get; protected set; }

    public DateTime CreatedDate { get; protected set; }


    public Comment(string text)
    {
      Condition.Requires(text, "text").IsNotNull();

      Text = text;
      CreatedDate = DateTime.Now;
    }
  }
}
