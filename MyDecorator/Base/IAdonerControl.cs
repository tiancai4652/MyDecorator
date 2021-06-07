using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDecorator.Base
{
    public interface IAdonerControl
    {
        Rect AdonerArrange(Size finalSize);
    }
}
