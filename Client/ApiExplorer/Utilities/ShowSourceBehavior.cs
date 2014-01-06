using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;


namespace ApiExplorer.Utilities
{
  public class ShowSourceBehavior : Behavior<FrameworkElement>
  {
    ShowSourceAdorner Adorner;

    // This is only here for the XAML source editor - it complains about this being a missing class if the constructor is not here
    public ShowSourceBehavior()
    {
    }

    public static DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(JToken), typeof(ShowSourceBehavior));

    public JToken Source
    {
      get { return (JToken)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }


    public static DependencyProperty PrefixProperty = DependencyProperty.RegisterAttached("Prefix", typeof(string), typeof(ShowSourceBehavior));

    public string Prefix
    {
      get { return (string)GetValue(PrefixProperty); }
      set { SetValue(PrefixProperty, value); }
    }


    protected override void OnAttached()
    {
      base.OnAttached();
      AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
      AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }


    void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
      if (Adorner == null)
      {
        Adorner = new ShowSourceAdorner(AssociatedObject);

        double w = AssociatedObject.DesiredSize.Width;
        double h = AssociatedObject.DesiredSize.Height;

        Border border = new Border
        {
          BorderBrush = Brushes.Green,
          BorderThickness = new Thickness(0,1,0,1),
          Width = w,
          Height = h
        };

        AssociatedObject.ToolTip = "Click to view source";
        AssociatedObject.MouseDown += ShowSource_Click;
        Adorner.Content = border;
      }

      Adorner.Content.Visibility = Visibility.Visible;
    }


    void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
      Adorner.Content.Visibility = Visibility.Collapsed;
    }


    void ShowSource_Click(object sender, RoutedEventArgs e)
    {
      JToken json = Source;
      if (json == null)
      {
        JsonViewModel jvm = AssociatedObject.DataContext as JsonViewModel;
        if (jvm != null && jvm.JsonValue != null)
          json = jvm.JsonValue;
      }

      ViewModel vm = AssociatedObject.DataContext as ViewModel;
      if (json != null && vm != null)
        vm.Publish(new MasonViewModel.SourceChangedEventArgs { Source = (Prefix ?? "") + json.ToString() });
    }
  }
}

