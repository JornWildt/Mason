
namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFileHandler
  {
    public object Get(string id)
    {
      return new Resources.CaseFileResource
      {
        ID = id,
        Description = "My case file"
      };
    }
  }
}
