namespace MasonBuilder.Net
{
  public class VoidAction : Control
  {
    public override string type { get { return "void"; } }

    public string method { get; set; }


    public VoidAction(string name, string href, string title = null, string method = null)
      : base(name, href, title)
    {
      this.method = method;
    }
  }
}
