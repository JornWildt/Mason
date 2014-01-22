using Mason.IssueTracker.Server.Utility;
using log4net;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.IO;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.IO;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssuesHandler : BaseHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(IssuesHandler));

    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }
    public IProjectRepository ProjectRepository { get; set; }
    public IAttachmentRepository AttachmentRepository { get; set; }

    #endregion


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        List<Issue> issues = IssueRepository.IssuesForProject(id);
        return new IssueCollectionResource
        {
          Issues = issues
        };
      });
    }


    public object Post(int id, CreateIssueArgs issue, IFile attachment)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Project p = ProjectRepository.Get(id);
        
        Issue i = new Issue(p, issue.Title, issue.Description, issue.Severity);
        IssueRepository.Add(i);

        using (Stream s = attachment.OpenStream())
        {
          byte[] content = s.ReadAllBytes();
          Attachment att = new Attachment(i, attachment.FileName, content, attachment.ContentType.MediaType);
          AttachmentRepository.Add(att);
        }

        Uri issueUrl = typeof(IssueResource).CreateUri(new { id = i.Id });

        return new OperationResult.Created { RedirectLocation = issueUrl };
      });
    }
  }
}
