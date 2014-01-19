using System;


namespace Mason.Net
{
  public class Action
  {
    public string name { get; set; }

    public string type { get; set; }

    public string href { get; set; }

    public string method { get; set; }

    public string description { get; set; }

    public string schema { get; set; }

    public string schemaUrl { get; set; }

    public object template { get; set; }


    public Action(string name, string type, Uri href, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      this.name = name;
      this.type = type;
      this.href = (href != null ? href.AbsoluteUri : null);
      this.description = description;
      this.schema = schema;
      this.schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null);
      this.template = template;
      this.method = method;
    }
  }
}
