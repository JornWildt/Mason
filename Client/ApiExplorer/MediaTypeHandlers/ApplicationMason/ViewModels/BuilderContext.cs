using ApiExplorer.Utilities;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class BuilderContext
  {
    public NamespaceManager Namespaces { get; set; }

    public BuilderContext()
    {
      Namespaces = new NamespaceManager();
    }
  }
}
