using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.Text.ViewModels
{
  public class TextViewModel : ViewModel
  {
    private string _text;
    public string Text
    {
      get { return _text; }
      set
      {
        if (value != _text)
        {
          _text = value;
          OnPropertyChanged("Text");
        }
      }
    }


    public TextViewModel(ViewModel parent, string text)
      : base(parent)
    {
      Text = text;
    }
  }
}
