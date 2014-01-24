using Newtonsoft.Json;


namespace Mason.Net
{
  public class Namespace
  {
    [JsonIgnore]
    public string prefix { get; set; }

    public string reference { get; set; }

    public Namespace()
    {
    }

    public Namespace(string alias, string value)
    {
      this.reference = value;
      this.prefix = alias;
    }
  }
}
