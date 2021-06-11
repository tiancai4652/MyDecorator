using MyDecorator.Base;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDecorator.Controls
{
    /// <summary>
    /// DragDropControl.xaml 的交互逻辑
    /// </summary>
    public partial class DragDropControl : UserControl, IAdonerControl
    {
        UIElement _adornedElement;
        public DragDropControl(UIElement adornedElement)
        {
            _adornedElement = adornedElement;
            InitializeComponent();
        }

        #region IAdonerControl
        public Rect AdonerArrange(Size finalSize)
        {
            int margin = 10;
            return new Rect(-margin, -margin, finalSize.Width + 2 * margin, finalSize.Height + 2 * margin);

        }



        #endregion

        bool isCanMove = false;

        private void thumMove_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isCanMove = true;
        }

        private void thumMove_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (isCanMove)
            {
                InkCanvas.SetLeft(_adornedElement, InkCanvas.GetLeft(_adornedElement) + e.HorizontalChange);
                InkCanvas.SetTop(_adornedElement, InkCanvas.GetTop(_adornedElement) + e.VerticalChange);
            }
        }

        private void thumMove_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isCanMove = false;
        }
    }
}
