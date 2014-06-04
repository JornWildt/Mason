using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.Html.ViewModels
{
  public class HtmlViewModel : ViewModel
  {
    private string _source;
    public string Source
    {
      get { return _source; }
      set
      {
        if (value != _source)
        {
          _source = value;
          OnPropertyChanged("Source");
        }
      }
    }


    public HtmlViewModel(ViewModel parent, string filename)
      : base(parent)
    {
      Source = filename;
    }
  }
}
