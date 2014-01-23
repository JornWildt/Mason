using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mason.IssueTracker.Server.Issues.Codecs
{
  public class IssueCollectionCodec : IssueTrackerMasonCodec<IssueCollectionResource>
  {
    protected override Resource ConvertToIssueTracker(IssueCollectionResource resource)
    {
      dynamic result = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        result.SetMeta(MasonProperties.MetaProperties.Title, "Project issues");
        result.SetMeta(MasonProperties.MetaProperties.Description, "This is the list of issues for a single project.");
      }

      //Uri selfUrl = typeof(IssueCollectionResource).CreateUri(new { id = resource.

      result.Issues = new List<SubResource>();

      foreach (Issue i in resource.Issues)
      {
        dynamic item = new SubResource();
        item.ID = i.Id.ToString();
        item.Title = i.Title;

        Uri itemSelfUri = typeof(IssueResource).CreateUri(new { id = i.Id });
        Link itemSelfLink = CommunicationContext.NewLink("self", itemSelfUri);
        item.AddLink(itemSelfLink);

        result.Issues.Add(item);
      }

      return result;
    }
  }
}
