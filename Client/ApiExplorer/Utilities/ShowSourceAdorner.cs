using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ApiExplorer.Utilities
{
  public class ShowSourceAdorner : Adorner
  {
    private AdornerLayer AdornerLayer;


    public ShowSourceAdorner(UIElement adornedElement)
      : base(adornedElement)
    {
      AdornerLayer = AdornerLayer.GetAdornerLayer(AdornedElement);
      AdornerLayer.Add(this);
    }


    private FrameworkElement _content;
    public FrameworkElement Content
    {
      get { return _content; }
      set
      {
        if (_content != null)
        {
          RemoveVisualChild(_content);
        }
        _content = value;
        if (_content != null)
        {
          AddVisualChild(_content);
        }
      }
    }


    protected override Size MeasureOverride(Size constraint)
    {
      Content.Measure(constraint);
      return Content.DesiredSize;
    }


    protected override Size ArrangeOverride(Size finalSize)
    {
      Content.Arrange(new Rect(new Point(0, 0), finalSize));
      return new Size(Content.ActualWidth, Content.ActualHeight);
    }


    protected override int VisualChildrenCount
    {
      get
      {
        return 1;
      }
    }


    protected override Visual GetVisualChild(int index)
    {
      if (index != 0) throw new ArgumentOutOfRangeException();
      return Content;
    }
  }

}
