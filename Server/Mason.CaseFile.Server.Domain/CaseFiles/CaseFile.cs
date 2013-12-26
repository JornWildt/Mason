using System;


namespace Mason.CaseFile.Server.Domain.CaseFiles
{
  public class CaseFile
  {
    public Guid Id { get; private set; }

    public string CaseNumber { get; internal set; }

    public string Title { get; protected set; }

    public string Description { get; protected set; }


    public CaseFile(string title, string description)
    {
      Id = Guid.NewGuid();
      Title = title;
      Description = description;
    }
  }
}
