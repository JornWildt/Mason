using Mason.Net;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.CaseFile.Contract
{
  [JsonObject(MemberSerialization.OptOut)]
  public class CaseFileCollection : Resource
  {
    public IList<CaseFileCollectionItem> CaseFiles { get; set; }

    public CaseFileCollection()
    {
      CaseFiles = new List<CaseFileCollectionItem>();
    }
  }

  
  [JsonObject(MemberSerialization.OptOut)]
  public class CaseFileCollectionItem : Resource
  {
    public string ID { get; set; }
    public string Title { get; set; }
  }
}
