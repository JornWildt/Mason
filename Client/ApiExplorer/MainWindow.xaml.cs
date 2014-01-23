using ApiExplorer.Properties;
using ApiExplorer.ViewModels;
using System.ComponentModel;
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

      Closing += MainWindow_Closing;
    }

    
    void MainWindow_Closing(object sender, CancelEventArgs e)
    {
      Settings.Default.Save();
    }
  }
}
