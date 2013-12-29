using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonViewModel : ViewModel
  {
    public string SourceText { get; set; }

    
    public MasonViewModel(ViewModel parent, string source)
      : base(parent)
    {
      SourceText = source;
    }
  }
}
