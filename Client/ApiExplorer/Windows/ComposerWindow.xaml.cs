using ApiExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ApiExplorer.Windows
{
  /// <summary>
  /// Interaction logic for ComposerWindow.xaml
  /// </summary>
  public partial class ComposerWindow : Window
  {
    public ComposerWindow(ComposerViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
      Loaded += ComposerWindow_Loaded;
    }

    
    void ComposerWindow_Loaded(object sender, RoutedEventArgs e)
    {
      MethodInput.Focus();
    }


    public static void OpenComposerWindow(
      Window owner, 
      ViewModel parent, 
      string method, 
      string url, 
      string windowTitle = null, 
      string body = null,
      string actionType = null)
    {
      ComposerViewModel vm = new ComposerViewModel(parent);
      if (method != null)
        vm.Method = method;
      if (url != null)
        vm.Url = url;
      if (body != null)
        vm.Body = body;
      if (actionType != null)
        vm.SelectedType = actionType;
      vm.WindowTitle = windowTitle ?? "Request composer";

      ComposerWindow w = new ComposerWindow(vm);
      w.Owner = owner;
      w.ShowDialog();
    }
  }
}
