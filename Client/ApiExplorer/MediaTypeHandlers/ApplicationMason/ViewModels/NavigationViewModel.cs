using ApiExplorer.ViewModels;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class NavigationViewModel : ElementViewModel
  {
    public string Name { get; set; }

    public string HRef { get { return GetValue<string>("href"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string NamePart1 { get; set; }

    public string NamePart2 { get; set; }

    public abstract string NavigationType { get; }


    #region Commands

    public DelegateCommand<object> ActivateNavigationCommand { get; private set; }

    #endregion



    public NavigationViewModel(ViewModel parent, JObject nav, string name, BuilderContext context)
      : base(parent, nav)
    {
      if (nav == null)
        throw new InvalidOperationException("Expected JSON object for " + NavigationType);

      string prefix;
      string reference;
      string nsname;

      Name = context.Namespaces.Expand(name, out prefix, out reference, out nsname);

      RegisterCommand(ActivateNavigationCommand = new DelegateCommand<object>(ActivateNavigation));

      //ToolTip1 = Title;
      //ToolTip2 += HRef;

      if (reference != null && nsname != null)
      {
        NamePart1 = nsname;
        NamePart2 = reference;
      }
      else
      {
        NamePart1 = "";
        NamePart2 = Name;
      }

      string target_type = GetValue<string>("target_type");
      if (!string.IsNullOrWhiteSpace(target_type))
        NamePart2 += " (" + target_type + ")";
    }


    protected abstract void ActivateNavigation(object args);
  }
}
