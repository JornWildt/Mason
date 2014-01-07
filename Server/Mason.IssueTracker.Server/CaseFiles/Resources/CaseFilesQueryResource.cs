using Mason.CaseFile.Server.Domain.CaseFiles;
using System;
using System.Collections.Generic;


namespace Mason.CaseFile.Server.CaseFiles.Resources
{
  public class CaseFilesQueryResource
  {
    public List<CaseFileListItem> CaseFiles { get; set; }
    
    public Uri SelfUri { get; set; }
  }
}
