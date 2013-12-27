using System;
using System.Collections.Generic;


namespace Mason.CaseFile.Server.Domain.CaseFiles
{
  public class CaseFileSearchArgs
  {
    public Guid? Id { get; set; }
    public string CaseNumber { get; set; }
    public string TextQuery { get; set; }
  }


  public interface ICaseFileRepository
  {
    CaseFile Get(Guid id);
    CaseFile GetByCaseNumber(string number);
    List<CaseFileListItem> FindCaseFiles(CaseFileSearchArgs args);
    void Add(CaseFile c);
  }
}
