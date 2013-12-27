using System.Collections.Generic;


namespace Mason.Net
{
  public class LinkTemplate
  {
    public string name { get; set; }

    public string template { get; set; }

    public string description { get; set; }

    public List<LinkTemplateParameter> parameters { get; private set; }


    public LinkTemplate(string name, string template, string description = null)
    {
      this.name = name;
      this.template = template;
      this.description = description;
      parameters = new List<LinkTemplateParameter>();
    }
  }


  public class LinkTemplateParameter
  {
    public string name { get; set; }

    public string type { get; set; }

    public string description { get; set; }

    public string value { get; set; }


    public LinkTemplateParameter(string name, string type = null, string description = null, string value = null)
    {
      this.name = name;
      this.type = type;
      this.description = description;
      this.value = value;
    }
  }
}
