using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.Image.ViewModels
{
  public class ImageViewModel : ViewModel
  {
    private string _filename;
    public string Filename
    {
      get { return _filename; }
      set
      {
        if (value != _filename)
        {
          _filename = value;
          OnPropertyChanged("Filename");
        }
      }
    }


    public ImageViewModel(ViewModel parent, string filename)
      : base(parent)
    {
      Filename = filename;
    }
  }
}
