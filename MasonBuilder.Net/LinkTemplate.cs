using Newtonsoft.Json;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public class LinkTemplate : Navigation
  {
    public override string type { get { return "link-template"; } }


    public List<LinkTemplateParameter> parameters { get; private set; }


    public LinkTemplate(string name, string template, string title = null)
      : base(name, template, title)
    {
    }


    public void AddParameter(LinkTemplateParameter p)
    {
      if (parameters == null)
        parameters = new List<LinkTemplateParameter>();
      parameters.Add(p);
    }
  }


  public class LinkTemplateParameter
  {
    public string name { get; set; }

    public string title { get; set; }

    public string description { get; set; }


    public LinkTemplateParameter(string name, string title = null, string description = null)
    {
      this.name = name;
      this.title = title;
      this.description = description;
    }
  }
}
