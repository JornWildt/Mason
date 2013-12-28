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


    public List<CaseFileListItem> FindCaseFiles(CaseFileSearchArgs args)
    {
      var q = CaseFiles.Where(c =>
        (args.Id == null || c.Id == args.Id)
        && (string.IsNullOrEmpty(args.CaseNumber) || c.CaseNumber == args.CaseNumber)
        && (string.IsNullOrEmpty(args.TextQuery) || CaseFileContainsText(c, args.TextQuery)));

      return q.Select(c => new CaseFileListItem { Id = c.Id, CaseNumber = c.CaseNumber, Title = c.Title, Description = c.Description }).ToList();
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


    private bool CaseFileContainsText(CaseFile c, string text)
    {
      return c != null && text != null &&
        (!string.IsNullOrEmpty(c.Title) && c.Title.ToLower().Contains(text.ToLower()) 
        || !string.IsNullOrEmpty(c.Description) && c.Description.ToLower().Contains(text.ToLower()));
    }
  }
}
