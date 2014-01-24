using System.Collections.Generic;
using CuttingEdge.Conditions;


namespace ApiExplorer.Utilities
{
  public class CurieManager
  {
    private Dictionary<string, string> Curies { get; set; }


    public CurieManager()
    {
      Curies = new Dictionary<string, string>();
    }


    public void AddCurie(string prefix, string reference)
    {
      Condition.Requires(prefix, "prefix").IsNotNull();
      Condition.Requires(reference, "reference").IsNotNull();
      Curies[prefix] = reference;
    }


    public string Expand(string curie)
    {
      if (curie == null)
        return null;

      int pos = curie.IndexOf(':');
      if (pos <= 0)
        return curie;

      string prefix = curie.Substring(0, pos);
      if (Curies.ContainsKey(prefix))
        return Curies[prefix] + curie.Substring(pos + 1);

      return curie;
    }
  }
}
