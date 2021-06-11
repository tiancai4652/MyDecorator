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
        public DragDropControl()
        {
            InitializeComponent();
        }

        #region IAdonerControl
        public Rect AdonerArrange(Size finalSize)
        {
            int margin = 10;
            return new Rect(-margin, -margin, finalSize.Width + 2 * margin, finalSize.Height + 2 * margin);

        }

        #endregion
    }
}
