using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.Comments;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;
using System.Text;


namespace Mason.IssueTracker.Server.Domain
{
  public static class DemoDataGenerator
  {
    public static void GenerateDemoData(
      IIssueRepository issueRepository, 
      IProjectRepository projectRepository, 
      IAttachmentRepository attachmentRepository)
    {
      Project p = new Project("SHOP", "Webshop", "All issues related to the webshop.");
      projectRepository.Add(p);

      Issue i = new Issue(p, "Crash after payment", @"I have justed paid for two pairs of shoes - or rather I tried to. When I clicked 'Pay' all I got was a yellow error screen.", 3);

      issueRepository.Add(i);

      string errorReport = "This is an error report ...";
      Attachment att = new Attachment(i, "Error report", "Error report from end user", Encoding.UTF8.GetBytes(errorReport), "text/plain");
      attachmentRepository.Add(att);

      string logFile = "DEBUG 2014-01-22 15:45:07,610 166033ms  [9] Log4NetTraceListener   WriteLine          - Executing OperationResult OperationResult: type=OK, statusCode=200.";
      att = new Attachment(i, "Logfile", "Logfile with server stack trace", Encoding.UTF8.GetBytes(logFile), "text/plain");
      attachmentRepository.Add(att);

      i = new Issue(p, "Not calculating VAT correctly", @"When I add both shoes and socks it fails to calculate the VAT correctly.", 3);

      issueRepository.Add(i);

      i = new Issue(p, "General Failure?", @"When I press ctrl-P it says 'General failure reading harddisk'! Who is that General and why is he reading my hard disk?", 5);

      issueRepository.Add(i);
    }
  }
}
