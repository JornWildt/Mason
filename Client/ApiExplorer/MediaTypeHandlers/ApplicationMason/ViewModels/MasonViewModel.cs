using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class MasonViewModel : ViewModel
  {
    public class SourceChangedEventArgs : EventArgs
    {
      public string Source { get; set; }
    }


    #region UI properties and Sub-viewmodels

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

    public ResourcePropertyViewModel MainProperty { get; set; }

    #endregion


    public MasonViewModel(ViewModel parent, JObject resource)
      : base(parent)
    {
      MainProperty = new ResourcePropertyViewModel(this, resource) { Name = "ROOT RESOURCE", Value = new ResourceViewModel(this, resource) };

      Subscribe<SourceChangedEventArgs>(e => Source = e.Source);

      // Extract meta title for window title and top-level property name
      if (resource[MasonProperties.Meta] != null && resource[MasonProperties.Meta][MasonProperties.MetaProperties.Title] != null)
      {
        string title = resource[MasonProperties.Meta][MasonProperties.MetaProperties.Title].Value<string>();
        if (!string.IsNullOrEmpty(title))
        {
          MainProperty.Name = title;
          Publish(new TitleChangedEventArgs { Title = title });
        }
      }
      else
      {
        Publish(new TitleChangedEventArgs { Title = "Unnamed resource" });
      }

      Source = resource.ToString();
    }
  }
}
