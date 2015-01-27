using Newtonsoft.Json;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public abstract class Control
  {
    [JsonIgnore]
    public string name { get; set; }

    public abstract string type { get; }

    public string href { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string[] formats { get; set; }


    public Control()
    {
    }


    public Control(string name, string href, string title = null)
    {
      this.name = name;
      this.href = href;
      this.title = title;
    }
  }
}
