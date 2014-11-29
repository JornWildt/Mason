using System.Collections.Generic;
using System.Dynamic;


namespace MasonBuilder.Net
{
  public class DynamicDictionary : DynamicObject
  {
    private Dictionary<string, object> Dictionary = new Dictionary<string, object>();


    public override IEnumerable<string> GetDynamicMemberNames()
    {
      return Dictionary.Keys;
    }


    public virtual void RegisterPropertyValue(string name, object value)
    {
      Dictionary[name] = value;
    }


    // If you try to get a value of a property  
    // not defined in the class, this method is called. 
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      string name = binder.Name;

      if (!Dictionary.TryGetValue(name, out result))
        result = null;

      return true;
    }

    
    // If you try to set a value of a property that is 
    // not defined in the class, this method is called. 
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      Dictionary[binder.Name] = value;

      // You can always add a value to a dictionary, 
      // so this method always returns true. 
      return true;
    }


    public object this[string key]
    {
      get
      {
        object value;
        if (Dictionary.TryGetValue(key, out value))
          return value;
        return null;
      }
      set
      {
        Dictionary[key] = value;
      }
    }
  }
}
