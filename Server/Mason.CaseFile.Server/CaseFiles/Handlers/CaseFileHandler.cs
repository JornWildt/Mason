using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Domain.CaseFiles;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFileHandler
  {
    public ICaseFileRepository CaseFileRepository { get; set; }


    public object Get(string id)
    {
      Domain.CaseFiles.CaseFile cf = CaseFileRepository.Get(new Guid(id));
      return new CaseFileResource
      {
        CaseFile = cf
      };
    }
  }
}
