using Mason.IssueTracker.Contract;
using Mason.IssueTracker.Server.Domain.Exceptions;
using Mason.IssueTracker.Server.JsonSchemas.Resources;


namespace Mason.IssueTracker.Server.JsonSchemas.Handlers
{
  public class SchemaHandler
  {
    public object Get(string name)
    {
      return new SchemaTypeResource { SchemaType = typeof(CreateProjectArgs) };
    }
  }
}
