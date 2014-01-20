using Mason.IssueTracker.Server.Domain.Comments;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;


namespace Mason.IssueTracker.Server.Domain
{
  public static class DemoDataGenerator
  {
    public static void GenerateDemoData(IIssueRepository issueRepository, IProjectRepository projectRepository)
    {
      Project p = new Project("P", "Project 1", "Blah ...");
      projectRepository.Add(p);

      Issue i = new Issue(p, "System.Reflection.BindingFlags Build Warnings on Windows Phone", @"Hey there,

Really great work on the library. I use it basically everywhere!
However, I encountered a build warning when I build my Windows Phone app, something like the following:

c:\a\src\external\packages\Newtonsoft.Json.4.5.11\lib\portable-net40+sl4+wp7+win8\Newtonsoft.Json.dll: Reference to type 'System.Reflection.BindingFlags' claims it is defined in 'c:\Program Files (x86)\Reference Assemblies\Microsoft\Framework.NETPortable\v4.5\Profile\Profile78\mscorlib.dll', but it could not be found

The message comes off a visual studio online build server but also repro on my local machine. Although it doesn't block me using the library (and all the functionality seems fine), I feel like reporting this anyway.

Thanks,
Chris", 3);

      issueRepository.Add(i);

      i = new Issue(p, "Incorrect AssemblyVersion", @"I have been having some problems with missing method exceptions from Json.Net 5.0.8 for methods that were added in v5, but only on some machines. It seemed the .net framework kept picking up the wrong Json dll.

Investigating it a bit i found that the Newtonsoft.Json.dll has AssemblyVersion 4.5.0.0 and an AssemblyFileVersion 5.0.8.16617. Since AssemblyVersion is what is used to by the .net framework to load the correct assembly, this seems to be able to cause some trouble. I assume that the AssemblyVersion should be 5.0.8.x?
", 3);

      issueRepository.Add(i);

      i = new Issue(p, "Schema Validation support for Draft 04", @"The current json.net JsonValidatingReader does not support the draft 04 schema per spec at json-schema.org. Specificaly the items I care about are that ""required"" field is now an array in draft04 and there is lack of support for the 'anyOf' keyword.", 3);

      issueRepository.Add(i);
    }
  }
}
