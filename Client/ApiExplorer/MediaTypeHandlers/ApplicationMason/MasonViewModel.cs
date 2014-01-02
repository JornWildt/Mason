﻿using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonViewModel : ViewModel
  {
    #region Sub-viewmodels

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
      MainProperty = new ResourcePropertyViewModel(this) { Name = "ROOT RESOURCE", Value = new ResourceViewModel(this, resource) };

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

      Source = resource.ToString();
    }
  }
}
