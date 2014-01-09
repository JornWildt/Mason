using log4net;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.Configuration;
using OpenRasta.DI;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.IssueTracker.Codecs;
using Mason.IssueTracker.Server.Domain.Comments;


namespace Mason.IssueTracker.Server.Issues
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting IssueTrackers");
      ResourceSpace.Uses.CustomDependency<IIssueRepository, IssueInMemoryRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<IssueResource>()
        .AtUri(UrlPaths.Issue)
        .HandledBy<IssueHandler>()
        .TranscodedBy<IssueCodec>();

      ResourceSpace.Has.ResourcesOfType<IssueQueryResource>()
        .AtUri(UrlPaths.IssueQuery)
        .HandledBy<IssueQueryHandler>()
        .TranscodedBy<IssueQueryCodec>();

      LoadDemoData();
    }


    private static void LoadDemoData()
    {
      IIssueRepository repo = new IssueInMemoryRepository();

      Issue i = new Issue("System.Reflection.BindingFlags Build Warnings on Windows Phone", @"Hey there,

Really great work on the library. I use it basically everywhere!
However, I encountered a build warning when I build my Windows Phone app, something like the following:

c:\a\src\external\packages\Newtonsoft.Json.4.5.11\lib\portable-net40+sl4+wp7+win8\Newtonsoft.Json.dll: Reference to type 'System.Reflection.BindingFlags' claims it is defined in 'c:\Program Files (x86)\Reference Assemblies\Microsoft\Framework.NETPortable\v4.5\Profile\Profile78\mscorlib.dll', but it could not be found

The message comes off a visual studio online build server but also repro on my local machine. Although it doesn't block me using the library (and all the functionality seems fine), I feel like reporting this anyway.

Thanks,
Chris", 3);

      repo.Add(i);

      i = new Issue("Incorrect AssemblyVersion", @"I have been having some problems with missing method exceptions from Json.Net 5.0.8 for methods that were added in v5, but only on some machines. It seemed the .net framework kept picking up the wrong Json dll.

Investigating it a bit i found that the Newtonsoft.Json.dll has AssemblyVersion 4.5.0.0 and an AssemblyFileVersion 5.0.8.16617. Since AssemblyVersion is what is used to by the .net framework to load the correct assembly, this seems to be able to cause some trouble. I assume that the AssemblyVersion should be 5.0.8.x?
", 3);

      i.AddComment(new Comment(@"Just realized this is done intentionally, so perhaps this doesn't qualify as an issue, but we have chosen to rollback to 4.5.1 to avoid the many issues we encountered because of the static assembly version. On one development machine we had to uninstall another product that was using json.net in order to get our unit tests running :) "));

      repo.Add(i);

      i = new Issue("Schema Validation support for Draft 04", @"The current json.net JsonValidatingReader does not support the draft 04 schema per spec at json-schema.org. Specificaly the items I care about are that ""required"" field is now an array in draft04 and there is lack of support for the 'anyOf' keyword.", 3);

      repo.Add(i);
    }
  }
}
