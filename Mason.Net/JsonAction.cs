namespace Mason.Net
{
  public class JsonAction : Navigation
  {
    public override string type { get { return "json"; } }

    public string method { get; set; }

    public string schema { get; set; }

    public string schemaUrl { get; set; }

    public object template { get; set; }


    public JsonAction(string name, string href, string title = null, string method = null)
      : base(name, href, title)
    {
      this.method = method;
    }
  }
}
