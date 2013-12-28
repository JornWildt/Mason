using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.ServiceIndex.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.ServiceIndex.Codecs
{
  public class ServiceIndexCodec : CaseFileMasonCodec<ServiceIndexResource>
  {
    public ICommunicationContext CommunicationContext { get; set; }


    protected override Net.Resource ConvertToCaseFile(ServiceIndexResource ServiceIndex)
    {
      Resource s = new Resource();

      s.SetMeta("mason:title", ServiceIndex.Title);
      s.SetMeta("mason:description", ServiceIndex.Description);

      string caseFileQueryUrl = CommunicationContext.ApplicationBaseUri.AbsoluteUri +"/" + UrlPaths.CaseFileQuery;
      LinkTemplate caseFileQueryTemplate = new LinkTemplate(RelTypes.CaseFileQuery, caseFileQueryUrl, "Search for case files");
      caseFileQueryTemplate.parameters.Add(new LinkTemplateParameter("id", description: "Case file ID"));
      caseFileQueryTemplate.parameters.Add(new LinkTemplateParameter("number", description: "Case file number"));
      caseFileQueryTemplate.parameters.Add(new LinkTemplateParameter("text", description: "Text query searching all relevante case file properties"));
      s.AddLinkTemplate(caseFileQueryTemplate);

      return s;
    }
  }
}
