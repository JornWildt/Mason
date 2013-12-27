using System.Collections.Generic;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFilesQueryHandler
  {
    public object Get(string id=null, string number=null)
    {
      return new Resources.CaseFilesResource
      {
        CaseFiles = new List<Resources.CaseFileResource>()
        {
        }
      };
    }
  }
}
