using Mason.CaseFile.Server.Utility;


namespace Mason.CaseFile.Server.Origin.Resources
{
  public class OriginResource : Mason.Net.Resource
  {
    // This is such a simple resource that we cheat and create the interface representation directly.
    public Contract.Origin Value { get; set; }
  }
}
