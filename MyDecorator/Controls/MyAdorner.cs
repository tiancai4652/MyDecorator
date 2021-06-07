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
    public class MyAdorner : Adorner
    {
        #region VisualCollection
        // Create a collection of child visual objects.
        private readonly VisualCollection _children;
        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount => _children.Count;

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        private readonly Canvas canvas;

        #endregion 

        public Border Border { get; set; }
        public MyAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Grid grid = new Grid();
          
            grid.Background = new SolidColorBrush(Colors.Red);
            var usercontrol = adornedElement as FrameworkElement;
            Canvas.SetLeft(grid, usercontrol.Margin.Left);
            Canvas.SetTop(grid, usercontrol.Margin.Top);
            grid.Width = usercontrol.Width+5;
            grid.Width = usercontrol.Height+5;

            canvas = new Canvas();
            canvas.Children.Add(grid);
            _children = new VisualCollection(this);
            _children.Add(canvas);
            //AddLogicalChild(canvas);
            //AddVisualChild(canvas);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

            //Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 1.0);

            //// Draw a circle at each corner.
            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X + 5, adornedElementRect.TopLeft.Y - 3));
            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y + 5));

            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X - 5, adornedElementRect.TopRight.Y - 3));
            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y + 5));

            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X + 5, adornedElementRect.BottomLeft.Y + 3));
            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y - 5));

            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X - 5, adornedElementRect.BottomRight.Y + 3));
            //drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y - 5));
        }


        private void Border_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Border_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
