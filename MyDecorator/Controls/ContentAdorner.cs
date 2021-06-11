using MyDecorator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MyDecorator.Controls
{
    /// <summary>
    /// 带内容控件的装饰器
    /// </summary>
    public class ContentAdorner : Adorner
    {
        UIElement ContentControl { set; get; }

        public ContentAdorner(UIElement adornedElement, UIElement contentControl) : base(adornedElement)
        {
            ContentControl = contentControl;
            visualChildren = new VisualCollection(this);
            visualChildren.Add(ContentControl);
        }

        #region Overrides

        VisualCollection visualChildren;
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (ContentControl is IAdonerControl)
            {
                ContentControl.Arrange((ContentControl as IAdonerControl).AdonerArrange(finalSize)); // you need to arrange

            }
            return finalSize;
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        #endregion
    }
}
