using System.Collections.Generic;
using CuttingEdge.Conditions;


namespace ApiExplorer.Utilities
{
  public class NamespaceManager
  {
    private Dictionary<string, string> Namespaces { get; set; }


    public NamespaceManager()
    {
      Namespaces = new Dictionary<string, string>();
    }


    public void Namespace(string prefix, string name)
    {
      Condition.Requires(prefix, "prefix").IsNotNull();
      Condition.Requires(name, "reference").IsNotNull();
      Namespaces[prefix] = name;
    }


    public string Expand(string curie)
    {
      string prefix;
      string reference;
      string nsname;
      return Expand(curie, out prefix, out reference, out nsname);
    }


    public string Expand(string curie, out string prefix, out string reference, out string nsname)
    {
      prefix = null;
      reference = null;
      nsname = null;

      if (curie == null)
        return null;

      int pos = curie.IndexOf(':');
      if (pos <= 0)
        return curie;

      prefix = curie.Substring(0, pos);
      reference = curie.Substring(pos + 1);

      if (Namespaces.ContainsKey(prefix))
      {
        nsname = Namespaces[prefix];
        return nsname + reference;
      }

      return curie;
    }
  }
}
