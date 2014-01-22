using ApiExplorer.ViewModels;
using System.Windows;


namespace ApiExplorer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      MainViewModel vm = new MainViewModel(this);
      DataContext = vm;
    }
  }
}
