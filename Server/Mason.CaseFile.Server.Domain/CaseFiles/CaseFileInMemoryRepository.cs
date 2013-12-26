using Mason.CaseFile.Server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.CaseFile.Server.Domain.CaseFiles
{
  public class CaseFileInMemoryRepository : ICaseFileRepository
  {
    #region ICaseFileRepository Members

    public CaseFile Get(Guid id)
    {
      CaseFile cf = Read(id);
      if (cf == null)
        throw new MissingResourceException("Unknown case file ID {0}.", id);
      return cf;
    }


    public CaseFile GetByCaseNumber(string number)
    {
      CaseFile cf = CaseFiles.Where(c => c.CaseNumber == number).FirstOrDefault();
      if (cf == null)
        throw new MissingResourceException("Unknown case file number {0}.", number);
      return cf;
    }


    public void Add(CaseFile c)
    {
      ++Key;
      c.CaseNumber = DateTime.Now.Year.ToString() + "-" + Key;
      CaseFiles.Add(c);
    }

    #endregion


    protected static int Key { get; set; }

    protected static List<CaseFile> CaseFiles { get; set; }


    static CaseFileInMemoryRepository()
    {
      Key = 0;
      CaseFiles = new List<CaseFile>();
    }


    private CaseFile Read(Guid id)
    {
      CaseFile cf = CaseFiles.Where(c => c.Id == id).FirstOrDefault();
      return cf;
    }
  }
}
