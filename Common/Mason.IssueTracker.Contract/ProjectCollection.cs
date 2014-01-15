using Mason.Net;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.IssueTracker.Contract
{
  public class ProjectCollection : Resource
  {
    [JsonProperty]
    public List<SubResource> Projects { get; set; }
  }
}
