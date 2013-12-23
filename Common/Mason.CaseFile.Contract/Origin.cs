using Mason.Net;
using Newtonsoft.Json;


namespace Mason.CaseFile.Contract
{
  [JsonObject(MemberSerialization.OptOut)]
  public class Origin : Resource
  {
    public string Title { get; set; }

    public string Description { get; set; }

    public Mason.Net.SubResource Contact { get; set; }
  }
}
