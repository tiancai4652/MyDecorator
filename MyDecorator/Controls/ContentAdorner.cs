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
    public class ContentAdorner : Adorner
    {
        UserControl ContentControl { set; get; }


        public ContentAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);

            // Add your UserControl or any visual here:
            ContentControl = new DragDropControl();
            //ContentControl.Width = adornedElement.RenderSize.Width + 100;
            //ContentControl.Height = adornedElement.RenderSize.Height + 100;
            ContentControl.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.1 };
            visualChildren.Add(ContentControl);
        }

        #region Overrides

        VisualCollection visualChildren;
        protected override Size ArrangeOverride(Size finalSize)
        {
            //// where to position the customControl...this is relative to the element you are adorning
            //double x = 0;
            //double y = 0;
            //customControl.Arrange(new Rect(x, y, 100, 100)); // you need to arrange

            //// Return the final size.
            //return finalSize;



            //ContentControl.Arrange(new Rect(finalSize));
            //return finalSize;

            if (ContentControl is IAdonerControl)
            {
                ContentControl.Arrange((ContentControl as IAdonerControl).AdonerArrange(finalSize)); // you need to arrange

            }
            return finalSize;
            //// where to position the customControl...this is relative to the element you are adorning
            //double x = 0;
            //double y = 0;
            //ContentControl.Arrange(new Rect(x, y, finalSize.Width + 100, finalSize.Height + 100)); // you need to arrange

            //// Return the final size.
            //return finalSize;
        }

        // Override the VisualChildrenCount and GetVisualChild properties to interface with
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        #endregion
    }
}
