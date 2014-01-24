using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;


namespace ApiExplorer.ValueConverters
{
  public class PathToImageConverter : ValueConverterBase
  {
    public PathToImageConverter()
    {
    }


    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string path = value as string;
      if (path != null)
      {
        BitmapImage image = new BitmapImage();
        using (FileStream stream = File.OpenRead(path))
        {
          try
          {
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            image.StreamSource = stream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit(); // load the image from the stream
          }
          catch (NotSupportedException)
          {
            return null;
          }
          catch (FileFormatException)
          {
            return null;
          }
        } // close the stream
        return image;
      }
      return null;
    }


    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
