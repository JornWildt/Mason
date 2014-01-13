using Mason.Net;
using Newtonsoft.Json;


namespace Mason.IssueTracker.Contract
{
  public class Project : Resource
  {
    [JsonProperty]
    public int Id { get; set; }

    [JsonProperty]
    public string Code { get; set; }

    [JsonProperty]
    public string Title { get; set; }

    [JsonProperty]
    public string Description { get; set; }
  }
}
