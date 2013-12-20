using System.Collections.Generic;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFilesHandler
  {
    public object Get()
    {
      return new Resources.CaseFilesResource
      {
        CaseFiles = new List<Resources.CaseFileResource>()
        {
          new Resources.CaseFileResource { ID = "1", Title = "Case 1", Description = "Blah 1 ..." },
          new Resources.CaseFileResource { ID = "2", Title = "Case 2", Description = "Blah 2 ..." },
        }
      };
    }
  }
}
