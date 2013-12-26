using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Domain.CaseFiles;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFileHandler
  {
    public ICaseFileRepository CaseFileRepository { get; set; }


    public object Get(string number)
    {
      Domain.CaseFiles.CaseFile cf = CaseFileRepository.GetByCaseNumber(number);
      return new CaseFileResource
      {
        CaseFile = cf
      };
    }
  }
}
