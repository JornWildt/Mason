using System.Collections.Generic;


namespace Mason.Net
{
  public class ActionFile
  {
    public string name { get; set; }

    public string description { get; set; }
  }


  public class JsonFilesAction : JsonAction
  {
    public override string type { get { return "json+files"; } }

    public List<ActionFile> files { get; set; }

    public string jsonFile { get; set; }


    public JsonFilesAction(string name, string href, string title = null, string method = null)
      : base(name, href, title, method)
    {
    }


    public void AddFile(string name, string description)
    {
      if (files == null)
        files = new List<ActionFile>();

      ActionFile file = new ActionFile { name = name, description = description };
      files.Add(file);
    }
  }
}
