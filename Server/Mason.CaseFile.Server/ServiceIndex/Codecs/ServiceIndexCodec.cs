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

      s.Meta["mason:title"] = ServiceIndex.Title;
      s.Meta["mason:description"] = ServiceIndex.Description;

      string caseFileQueryUrl = CommunicationContext.ApplicationBaseUri.AbsoluteUri +"/" + UrlPaths.CaseFileQuery;
      LinkTemplate caseFileQueryTemplate = new LinkTemplate("cf:case-file-query", caseFileQueryUrl, "Search for case files");
      caseFileQueryTemplate.parameters.Add(new LinkTemplateParameter("id"));
      caseFileQueryTemplate.parameters.Add(new LinkTemplateParameter("number"));
      s.LinkTemplates.Add(caseFileQueryTemplate);

      return s;
    }
  }
}
