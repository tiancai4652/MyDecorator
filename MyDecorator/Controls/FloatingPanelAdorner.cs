using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MyDecorator.Controls
{
    public class FloatingPanelAdorner : Adorner
    {
        UserControl customControl;
        VisualCollection visualChildren;

        public FloatingPanelAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);

            // Add your UserControl or any visual here:
            customControl = new UserControl();
            customControl.Width = customControl.Height = 500;
            customControl.Background = new SolidColorBrush(Colors.Black);
            visualChildren.Add(customControl);
        }

        #region Overrides

        protected override Size ArrangeOverride(Size finalSize)
        {
            //// where to position the customControl...this is relative to the element you are adorning
            //double x = 0;
            //double y = 0;
            //customControl.Arrange(new Rect(x, y, 100, 100)); // you need to arrange

            //// Return the final size.
            //return finalSize;

            customControl.Arrange(new Rect(finalSize));
            return finalSize;
        }

        // Override the VisualChildrenCount and GetVisualChild properties to interface with
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        #endregion
    }
}
