using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApiExplorer.ValueConverters
{
  public class EmptyStringToVisibility : ValueConverterBase
  {
    private bool Invert = false;


    public EmptyStringToVisibility()
    {
    }


    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is string && (string)parameter == "InverseVisibility")
        Invert = true;

      bool empty = string.IsNullOrEmpty((string)value);
      if (Invert)
        return empty ? Visibility.Visible : Visibility.Collapsed;
      else
        return empty ? Visibility.Collapsed : Visibility.Visible;
    }


    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
