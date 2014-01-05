using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class ResourceViewModel : JsonViewModel
  {
    #region UI properties

    public ObservableCollection<LinkViewModel> Links { get; private set; }

    public bool HasLinks { get { return Links != null && Links.Count > 0; } }

    public JToken LinksJsonValue { get; private set; }

    public ObservableCollection<LinkTemplateViewModel> LinkTemplates { get; private set; }

    public bool HasLinkTemplates { get { return LinkTemplates != null && LinkTemplates.Count > 0; } }

    public JToken LinkTemplatesJsonValue { get; private set; }

    public ObservableCollection<ViewModel> Properties { get; private set; }

    public string Description { get; set; }

    public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }

    public JToken MetaJsonValue { get; private set; }

    #endregion


    #region Commands

    #endregion


    public ResourceViewModel(ViewModel parent, JObject resource)
      : base(parent, resource)
    {
      Properties = new ObservableCollection<ViewModel>();

      foreach (var pair in resource)
      {
        if (pair.Key == MasonProperties.Links && pair.Value is JArray)
        {
          LinksJsonValue = pair.Value;
          Links = new ObservableCollection<LinkViewModel>(
            pair.Value.Children().OfType<JObject>().Select(l => new LinkViewModel(this, l)));
        }
        else if (pair.Key == MasonProperties.LinkTemplates && pair.Value is JArray)
        {
          LinkTemplatesJsonValue = pair.Value;
          LinkTemplates = new ObservableCollection<LinkTemplateViewModel>(
            pair.Value.Children().OfType<JObject>().Select(l => new LinkTemplateViewModel(this, l)));
        }
        else if (pair.Key == MasonProperties.Namespaces && pair.Value is JArray)
        {
        }
        else if (pair.Key == MasonProperties.Meta && pair.Value is JObject)
        {
          MetaJsonValue = pair.Value;
          Description = GetValue<string>(pair.Value, MasonProperties.MetaProperties.Description);
        }
        else
        {
          if (pair.Value is JArray)
          {
          }
          else if (pair.Value is JObject)
          {
            Properties.Add(new ResourcePropertyViewModel(this, pair.Value) { Name = pair.Key, Value = new ResourceViewModel(this, (JObject)pair.Value) });
          }
          else
            Properties.Add(new PropertyViewModel(this, pair.Value) { Name = pair.Key, Value = (pair.Value != null ? pair.Value.ToString() : "") });
        }
      }

      if (Links == null)
        Links = new ObservableCollection<LinkViewModel>();
    }
  }
}
