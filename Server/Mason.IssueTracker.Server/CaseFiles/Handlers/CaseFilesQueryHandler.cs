using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Domain.CaseFiles;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFilesQueryHandler
  {
    #region Dependencies

    public ICaseFileRepository CaseFileRepository { get; set; }

    #endregion


    public object Get(string id=null, string number=null, string text=null)
    {
      Guid? gid = (!string.IsNullOrEmpty(id) ? new Guid(id) : (Guid?)null);
      CaseFileSearchArgs args = new CaseFileSearchArgs { Id = gid, CaseNumber = number, TextQuery = text };

      Uri selfUri = typeof(CaseFilesQueryResource).CreateUri(new { id = id, number = number, text = text });

      List<CaseFileListItem> caseFiles = CaseFileRepository.FindCaseFiles(args);
      return new Resources.CaseFilesQueryResource
      {
        CaseFiles = caseFiles,
        SelfUri = selfUri
      };
    }
  }
}
