namespace MasonBuilder.Net
{
  public class MetaElement : SubResource
  {
    public string Title
    {
      get { return (string)this[MasonProperties.MetaProperties.Title]; }
      set { this[MasonProperties.MetaProperties.Title] = value; }
    }


    public string Description
    {
      get { return (string)this[MasonProperties.MetaProperties.Description]; }
      set { this[MasonProperties.MetaProperties.Description] = value; }
    }
  }
}
