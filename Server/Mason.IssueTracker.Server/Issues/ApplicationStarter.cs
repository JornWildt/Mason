using log4net;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.Configuration;
using OpenRasta.DI;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.IssueTracker.Codecs;


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
        .AtUri("/issues/{id}")
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

      Issue i = new Issue("CSS viewport units (vw, vh, vmin, vmax) flicker on small font-size", @"UserAgent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36

Example URL:
http://jsfiddle.net/N9jPu/3/show/

Steps to reproduce the problem:
1. Open test case http://jsfiddle.net/N9jPu/3/show/ on latest Canary build
2. Resize document width (screen size) from  284px - 275px
3. You will see that document width continues to scale while font-size hangs on the same size 28px and sometimes is dropped on new line - it quite annoying to see text one word jumping from one line to another back and forward

What is the expected behavior?

What went wrong?
CSS viewport units (vw, vh, vmin and vmax) support landed in http://src.chromium.org/viewvc/blink?view=rev&rev=164448

Test case http://jsfiddle.net/N9jPu/3/show/ repreduces flicker on small font-size while resizing screen size from 270px to smaller - text ""properlly"" and ""WHY?"" jumps from one line to another back and forward.
I think http://src.chromium.org/viewvc/blink?view=rev&rev=164448 still needs some optimizations if possible, it seems as a sub-pixel issue.
");

      repo.Add(i);
    }
  }
}
