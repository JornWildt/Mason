
namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFileHandler
  {
    public object Get()
    {
      return new CaseFiles.Resources.CaseFileResource
      {
        ID = "10",
        Description = "My case file"
      };
    }
  }
}
