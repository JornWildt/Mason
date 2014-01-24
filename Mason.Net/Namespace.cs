using Newtonsoft.Json;


namespace Mason.Net
{
  public class Namespace
  {
    [JsonIgnore]
    public string prefix { get; set; }

    public string name { get; set; }

    public Namespace()
    {
    }

    public Namespace(string prefix, string name)
    {
      this.name = name;
      this.prefix = prefix;
    }
  }
}
