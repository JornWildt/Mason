﻿using ApiExplorer.ViewModels;
using MasonBuilder.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class ResourceViewModel : JsonViewModel
  {
    #region UI properties

    public ObservableCollection<NavigationViewModel> Navigation { get; private set; }

    public bool HasNavigation { get { return Navigation != null && Navigation.Count > 0; } }

    public JToken NavigationJsonValue { get; private set; }

    public ObservableCollection<ViewModel> Properties { get; private set; }

    public string Description { get; set; }

    public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }

    public ObservableCollection<LinkViewModel> MetaLinks { get; private set; }

    public bool HasMetaLinks { get { return MetaLinks != null && MetaLinks.Count > 0; } }

    public JToken MetaLinksJsonValue { get; private set; }

    public JToken MetaJsonValue { get; private set; }

    #endregion


    #region Commands

    #endregion


    public ResourceViewModel(ViewModel parent, JObject resource, BuilderContext context)
      : base(parent, resource)
    {
      Properties = new ObservableCollection<ViewModel>();

      foreach (var pair in resource)
      {
        if (pair.Key == MasonProperties.Namespaces && pair.Value is JObject)
        {
          BuildNamespaces((JObject)pair.Value, context);
        }
        else if (pair.Key == MasonProperties.Navigation && pair.Value is JObject)
        {
          NavigationJsonValue = pair.Value;
          Navigation = new ObservableCollection<NavigationViewModel>(
            pair.Value.Children().OfType<JProperty>().Select(n => BuildNavigationElement(this, n, context)).Where(n => n != null));
        }
        else if (pair.Key == MasonProperties.Meta && pair.Value is JObject)
        {
          MetaJsonValue = pair.Value;
          Description = GetValue<string>(pair.Value, MasonProperties.MetaProperties.Description);
          JToken metaLinksProperty = pair.Value[MasonProperties.Navigation];
          if (metaLinksProperty is JObject)
          {
            MetaLinksJsonValue = metaLinksProperty;
            MetaLinks = new ObservableCollection<LinkViewModel>(
              metaLinksProperty.Children().OfType<JProperty>().Select(l => new LinkViewModel(this, l, context)));
          }
        }
        else if (pair.Key == MasonProperties.Error && pair.Value is JObject)
        {
          ResourcePropertyViewModel error = new ResourcePropertyViewModel(this, pair.Value, pair.Key, new ResourceViewModel(this, (JObject)pair.Value, context));
          error.IsError = true;
          Properties.Add(error);
        }
        else
        {
          Properties.Add(CreatePropertiesRecursively(pair.Key, pair.Value, context));
        }
      }

      if (Navigation == null)
        Navigation = new ObservableCollection<NavigationViewModel>();
    }

    
    private NavigationViewModel BuildNavigationElement(ResourceViewModel parent, JProperty n, BuilderContext context)
    {
      string type = GetValue<string>(n.Value, "type");
      if (type == MasonProperties.NavigationTypes.Link)
        return new LinkViewModel(parent, n, context);
      else if (type == MasonProperties.NavigationTypes.LinkTemplate)
        return new LinkTemplateViewModel(parent, n, context);
      else if (type == MasonProperties.NavigationTypes.Void)
        return new VoidActionViewModel(parent, n, context);
      else if (type == MasonProperties.NavigationTypes.JSON)
        return new JsonActionViewModel(parent, n, context);
      else if (type == MasonProperties.NavigationTypes.JSONFiles)
        return new JsonFilesActionViewModel(parent, n, context);

      return null;
    }


    private PropertyViewModel CreatePropertiesRecursively(string name, JToken json, BuilderContext context)
    {
      if (json is JArray)
      {
        int index=0;
        ObservableCollection<ViewModel> array = new ObservableCollection<ViewModel>(json.Select(i => CreatePropertiesRecursively(string.Format("[{0}]",index++), i, context)));
        return new ArrayPropertyViewModel(this, json, name, array);
      }
      else if (json is JObject)
      {
        return new ResourcePropertyViewModel(this, json, name, new ResourceViewModel(this, (JObject)json, context));
      }
      else
        return new PropertyViewModel(this, json, name, (json != null ? json.ToString() : ""));
    }


    private void BuildNamespaces(JObject namespaces, BuilderContext context)
    {
      foreach (JProperty ns in namespaces.Properties())
      {
        JObject nsDef = ns.Value as JObject;
        if (nsDef != null)
        {
          JToken jsonName = nsDef[MasonProperties.NamespaceProperties.Name];
          string ns_name = (jsonName != null && jsonName.Type == JTokenType.String ? jsonName.Value<string>() : null);
          string ns_prefix = ns.Name;
          if (!string.IsNullOrWhiteSpace(ns_prefix) && !string.IsNullOrWhiteSpace(ns_name))
            context.Namespaces.Namespace(ns_prefix, ns_name);
        }
      }
    }
  }
}
