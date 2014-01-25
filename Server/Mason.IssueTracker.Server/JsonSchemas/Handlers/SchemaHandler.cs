using Mason.IssueTracker.Server.Domain.Exceptions;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;


namespace Mason.IssueTracker.Server.JsonSchemas.Handlers
{
  public class SchemaHandler
  {
    // Not the best implementation, but its not important right now ...

    public object Get(string name)
    {
      if (name != null && name.EndsWith(".txt"))
        name = name.Substring(0, name.Length - 4);
      else if (name != null && name.EndsWith(".json"))
        name = name.Substring(0, name.Length - 5);

      if (name == "create-project")
        return new SchemaTypeResource { SchemaType = typeof(AddProjectArgs) };
      else if (name == "create-issue")
        return new SchemaTypeResource { SchemaType = typeof(AddIssueArgs) };
      else if (name == "create-attachment")
        return new SchemaTypeResource { SchemaType = typeof(AddAttachmentArgs) };
      
      return null;
    }
  }
}
