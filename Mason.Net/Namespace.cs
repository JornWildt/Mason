namespace Mason.Net
{
  public class Namespace
  {
    public string alias { get; set; }

    public string value { get; set; }

    public Namespace()
    {
    }

    public Namespace(string alias, string value)
    {
      this.value = value;
      this.alias = alias;
    }
  }
}
