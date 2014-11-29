using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
