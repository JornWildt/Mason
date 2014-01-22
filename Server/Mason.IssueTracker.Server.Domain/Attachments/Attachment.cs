﻿using CuttingEdge.Conditions;
using Mason.IssueTracker.Server.Domain.Issues;
using System;


namespace Mason.IssueTracker.Server.Domain.Attachments
{
  public class Attachment
  {
    public const int MaxContentLength = 1000;

    public virtual int Id { get; protected set; }

    public virtual Issue OwnerIssue { get; protected set; }

    public virtual string Title { get; protected set; }

    public virtual byte[] Content { get; protected set; }
    
    public virtual string ContentType { get; protected set; }

    public virtual long ContentLength { get; protected set; }

    public virtual DateTime CreatedDate { get; protected set; }


    public Attachment()
    {
    }


    public Attachment(Issue owner, string title, byte[] content, string contentType)
    {
      Condition.Requires(owner, "owner").IsNotNull();
      OwnerIssue = owner;
      Update(title, content, contentType);
      CreatedDate = DateTime.Now;
    }


    public virtual void Update(string title, byte[] content, string contentType)
    {
      ErrorHandling.ValidateInput(
        () => Condition.Requires(title, "title").IsNotNullOrWhiteSpace().IsNotLongerThan(255),
        () => Condition.Requires(content, "content").IsNotNull().IsNotLongerThan(MaxContentLength),
        () => Condition.Requires(contentType, "contentType").IsNotNullOrWhiteSpace());

      Title = title;
      Content = content;
      ContentType = contentType;
      ContentLength = content.Length;
    }
  }
}
