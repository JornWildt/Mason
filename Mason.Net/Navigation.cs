using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.Net
{
  public abstract class Navigation
  {
    [JsonIgnore]
    public string name { get; set; }

    public abstract string type { get; }

    public string href { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string target_type { get; set; } // FIXME


    public Navigation()
    {
    }


    public Navigation(string name, string href, string title = null)
    {
      this.name = name;
      this.href = href;
      this.title = title;
    }
  }
}
