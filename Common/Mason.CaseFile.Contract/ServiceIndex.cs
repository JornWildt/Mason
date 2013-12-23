using Mason.Net;
using Newtonsoft.Json;


namespace Mason.CaseFile.Contract
{
  [JsonObject(MemberSerialization.OptOut)]
  public class ServiceIndex : Resource
  {
    public string Title { get; set; }

    public string Description { get; set; }
  }
}
