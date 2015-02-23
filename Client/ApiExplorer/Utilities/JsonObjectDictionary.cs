using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiExplorer.Utilities
{
  public class JsonObjectDictionary : IDictionary<string, object>
  {
    protected JObject JSON { get; set; }


    public JsonObjectDictionary(JObject json)
    {
      JSON = json;
    }


    #region IDictionary<string,object> Members

    public void Add(string key, object value)
    {
      throw new NotImplementedException();
    }

    public bool ContainsKey(string key)
    {
      JToken token;
      return JSON.TryGetValue(key, out token);
    }

    public ICollection<string> Keys
    {
      get { return JSON.Properties().Select(p => p.Name).ToList(); }
    }

    public bool Remove(string key)
    {
      throw new NotImplementedException();
    }

    public bool TryGetValue(string key, out object value)
    {
      JToken token;
      if (JSON.TryGetValue(key, out token))
      {
        if (token is JObject)
        {
          value = new JsonObjectDictionary(token as JObject);
          return true;
        }
        else if (token is JValue)
        {
          value = ((JValue)token).Value;
          return true;
        }
      }
      value = null;
      return false;
    }

    public ICollection<object> Values
    {
      get { return JSON.Values().Cast<object>().ToList(); }
    }

    public object this[string key]
    {
      get
      {
        return JSON.GetValue(key);
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion

    #region ICollection<KeyValuePair<string,object>> Members

    public void Add(KeyValuePair<string, object> item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { return JSON.Count; }
    }

    public bool IsReadOnly
    {
      get { return true; }
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<KeyValuePair<string,object>> Members

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
