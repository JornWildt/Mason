using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.Net
{
  public class LinkTemplate
  {
    [JsonIgnore]
    public string name { get; set; }

    public string template { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public List<LinkTemplateParameter> parameters { get; private set; }


    public LinkTemplate(string name, string template, string title = null, string description = null)
    {
      this.name = name;
      this.template = template;
      this.title = title;
      this.description = description;
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

    public string description { get; set; }


    public LinkTemplateParameter(string name, string description = null)
    {
      this.name = name;
      this.description = description;
    }
  }
}
