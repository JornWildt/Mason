using System;
using System.Globalization;
using System.Windows;


namespace ApiExplorer.ValueConverters
{
  public class BoolToVisibilityConverter : ValueConverterBase
  {
    private bool Invert = false;


    public BoolToVisibilityConverter()
    {
    }


    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is string && (string)parameter == "InverseVisibility")
        Invert = true;

      bool visible = (bool)value;
      if (Invert)
        return visible ? Visibility.Collapsed : Visibility.Visible;
      else
        return visible ? Visibility.Visible : Visibility.Collapsed;
    }


    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is string && (string)parameter == "InverseVisibility")
        Invert = true;

      Visibility visibility = (Visibility)value;
      if (Invert)
        return visibility != Visibility.Visible;
      else
        return visibility == Visibility.Visible;
    }
  }
}
