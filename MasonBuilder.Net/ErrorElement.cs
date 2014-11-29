using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MasonBuilder.Net
{
  public class ErrorElement : SubResource
  {
    public string Id
    {
      get { return (string)this[MasonProperties.ErrorProperties.Id]; }
      set { this[MasonProperties.ErrorProperties.Id] = value; }
    }


    public string Code
    {
      get { return (string)this[MasonProperties.ErrorProperties.Code]; }
      set { this[MasonProperties.ErrorProperties.Code] = value; }
    }


    public string Message
    {
      get { return (string)this[MasonProperties.ErrorProperties.Message]; }
      set { this[MasonProperties.ErrorProperties.Message] = value; }
    }


    public string Details
    {
      get { return (string)this[MasonProperties.ErrorProperties.Details]; }
      set { this[MasonProperties.ErrorProperties.Details] = value; }
    }


    public DateTime? Time
    {
      get { return (DateTime?)this[MasonProperties.ErrorProperties.Time]; }
      set { this[MasonProperties.ErrorProperties.Time] = value; }
    }
  }
}
