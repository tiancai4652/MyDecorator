using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyDecorator.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {

        Point startPoint;
        Rectangle selectRect;
        bool startDraw = false;

        private DelegateCommand<MouseButtonEventArgs> _GridMouseDownCommand;
        public DelegateCommand<MouseButtonEventArgs> GridMouseDownCommand =>
            _GridMouseDownCommand ?? (_GridMouseDownCommand = new DelegateCommand<MouseButtonEventArgs>(ExecuteGridMouseDownCommand));

        void ExecuteGridMouseDownCommand(MouseButtonEventArgs parameter)
        {
            startPoint = parameter.GetPosition(Application.Current.MainWindow);
            startDraw = true;
            Message = startPoint.ToString();
        }

        private DelegateCommand<MouseEventArgs> _GridMouseMoveCommand;
        public DelegateCommand<MouseEventArgs> GridMouseMoveCommand =>
            _GridMouseMoveCommand ?? (_GridMouseMoveCommand = new DelegateCommand<MouseEventArgs>(ExecuteGridMouseMoveCommand));

        void ExecuteGridMouseMoveCommand(MouseEventArgs parameter)
        {
            if (startDraw)
            {
                Point tempEndPoint = parameter.GetPosition(Application.Current.MainWindow);
                DrawRect(tempEndPoint, startPoint);
            }
        }

        private DelegateCommand<MouseButtonEventArgs> _GridMouseUpCommand;
        public DelegateCommand<MouseButtonEventArgs> GridMouseUpCommand =>
            _GridMouseUpCommand ?? (_GridMouseUpCommand = new DelegateCommand<MouseButtonEventArgs>(ExecuteGridMouseUpCommand));

        void ExecuteGridMouseUpCommand(MouseButtonEventArgs parameter)
        {
            if (startDraw)
            {
                Controls.Remove(selectRect);
                selectRect = null;
                startDraw = false;
            }
        }

        private void DrawRect(Point endPoint, Point startPoint)
        {
            if (selectRect == null)
            {
                selectRect = new Rectangle();
                selectRect.Fill = new SolidColorBrush(Colors.Gray) { Opacity = 0.1 };
                selectRect.Margin = new Thickness(startPoint.X, startPoint.Y, 0, 0);
                Controls.Add(selectRect);
            }
            selectRect.Width = Math.Abs(endPoint.X - startPoint.X);
            selectRect.Height = Math.Abs(endPoint.Y - startPoint.Y);
        }
    }
}
