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
    public ComposerWindow(ComposerViewModel vm, StartFocus focus)
    {
      InitializeComponent();
      DataContext = vm;
      Loaded += (s,e) => ComposerWindow_Loaded(focus);
    }


    void ComposerWindow_Loaded(StartFocus focus)
    {
      if (focus == StartFocus.Method)
        MethodInput.Focus();
      else
      {
        if (TextEditorInput.Visibility == System.Windows.Visibility.Visible)
          TextEditorInput.SetFocus();
        else if (TextWithFilesEditorInput.Visibility == System.Windows.Visibility.Visible)
          TextWithFilesEditorInput.SetFocus();
      }
    }


    public enum StartFocus { Method, Body }

    public static void OpenComposerWindow(
      Window owner, 
      ViewModel parent, 
      string method, 
      string url, 
      string windowTitle = null, 
      string body = null,
      string actionType = null,
      Action<ComposerViewModel> modifier = null,
      StartFocus focus = StartFocus.Method)
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

      if (modifier != null)
        modifier(vm);

      ComposerWindow w = new ComposerWindow(vm, focus);
      w.Owner = owner;
      w.ShowDialog();
    }
  }
}
