using System;
using System.IO;


namespace Mason.IssueTracker.Server.Utility
{
  public static class StreamExtensions
  {
    public static byte[] ReadAllBytes(this Stream stream)
    {
      stream.Position = 0;
      byte[] buffer = new byte[stream.Length];
      for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
        totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
      return buffer;
    }
  }
}
