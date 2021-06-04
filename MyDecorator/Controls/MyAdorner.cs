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
        public Border Border { get; set; }
        public MyAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(20);
            border.BorderBrush = new SolidColorBrush(Colors.Orange) { Opacity = 0.5 };
            Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);
            border.Child = new ContentControl() { Content = adornedElementRect };
            border.MouseDown += Border_MouseDown;
            border.MouseUp += Border_MouseUp;
            border.MouseMove += Border_MouseMove;
            AddLogicalChild(border);
            AddVisualChild(border);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 1.0);

            // Draw a circle at each corner.
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X + 5, adornedElementRect.TopLeft.Y - 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y + 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X - 5, adornedElementRect.TopRight.Y - 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y + 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X + 5, adornedElementRect.BottomLeft.Y + 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y - 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X - 5, adornedElementRect.BottomRight.Y + 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y - 5));
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
