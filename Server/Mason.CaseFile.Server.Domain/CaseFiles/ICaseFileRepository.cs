using System;


namespace Mason.CaseFile.Server.Domain.CaseFiles
{
  public interface ICaseFileRepository
  {
    CaseFile Get(Guid id);
    CaseFile GetByCaseNumber(string number);
    void Add(CaseFile c);
  }
}
