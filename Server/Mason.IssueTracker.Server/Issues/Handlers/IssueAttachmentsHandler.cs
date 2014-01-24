using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueAttachmentsHandler : BaseHandler
  {
    public object Post(int id, AddAttachmentArgs args, IFile attachment)
    {
      return null;
    }
  }
}
