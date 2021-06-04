using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyDecorator.Base
{
    public interface IManipulation
    {
        //public ObservableCollection<AdornerFounction> Founctions { get; set; }

        void TransformAt(Transform transform);

        int GetZIndex();

        bool GetCross(Rectangle rectangle);

        void SetZIndex(int zindex);

        object Copy();

        Rect GetRect();
    }
}
