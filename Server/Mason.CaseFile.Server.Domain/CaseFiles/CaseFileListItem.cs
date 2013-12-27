using System;


namespace Mason.CaseFile.Server.Domain.CaseFiles
{
  public class CaseFileListItem
  {
    public Guid Id { get; set; }

    public string CaseNumber { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
  }
}
