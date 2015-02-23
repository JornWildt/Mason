using Newtonsoft.Json;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public class FileDefinition
  {
    public string name { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string[] accept { get; set; }

    public string schema { get; set; }

    public string schemaUrl { get; set; }

    public string template { get; set; }
  }


  public class Control
  {
    [JsonIgnore]
    public string name { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string serialization { get; set; }

    public string href { get; set; }

    /// <summary>
    /// This is the internal isHrefTemplate value.
    /// </summary>
    [JsonIgnore]
    public bool isHrefTemplateValue { get; set; }

    /// <summary>
    /// This is the external isHrefTemplate value for the serializer.
    /// </summary>
    public bool? isHrefTemplate
    {
      get { return isHrefTemplateValue ? (bool?)true : null; }
    }

    public string method { get; set; }

    public object schema { get; set; }

    public string schemaUrl { get; set; }

    public object template { get; set; }

    public string[] accept { get; set; }

    public string[] output { get; set; }

    public List<FileDefinition> files { get; set; }

    public List<Control> alt { get; set; }


    public Control()
    {
    }


    public Control(string name, string href, string title = null, bool isHrefTemplate = false)
    {
      this.name = name;
      this.href = href;
      this.isHrefTemplateValue = isHrefTemplate;
      this.title = title;
    }


    public void AddFile(FileDefinition p)
    {
      if (files == null)
        files = new List<FileDefinition>();
      files.Add(p);
    }


    public void AddAlternateControl(Control c)
    {
      if (alt == null)
        alt = new List<Control>();
      alt.Add(c);
    }
  }
}

