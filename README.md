# 装饰器学习笔记

### 0 定义

装饰器可以用来给显示的控件添加一些装饰的效果；

### 1 效果

![effect](https://github.com/tiancai4652/MyDecorator/blob/master/MyDecorator/Other/ConnonAdernor.gif)

#### 1 框选

鼠标按下获取当前点

```c#
   startPoint = parameter.GetPosition(Application.Current.MainWindow);
```

随着鼠标移动获取末位点，不断重新画矩形

```
 Point tempEndPoint = parameter.GetPosition(Application.Current.MainWindow);
```

#### 1.1 画临时矩形

```
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
```

#### 2 添加装饰器

装饰器是 [FrameworkElement](https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.frameworkelement?view=net-5.0) 绑定到的自定义 [UIElement](https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.uielement?view=net-5.0) ， 装饰器呈现在装饰器层中。

- 装饰器层是始终位于装饰元素或装饰元素集合之上。 
- 装饰器通常使用位于装饰元素左上部的标准 2D 坐标原点，相对于其绑定到的元素进行定位。
- 装饰器始终以可见的方式位于控件顶部，无法使用 z 顺序重写。

2.1 常规添加装饰器

![ConnonAdernor](https://github.com/tiancai4652/MyDecorator/blob/master/MyDecorator/Other/ConnonAdernor.gif)

常规做法一般在装饰器层添加装饰器后，在装饰器中会以重写Render的方式添加效果

```c#
// Adorners must subclass the abstract base class Adorner.
public class SimpleCircleAdorner : Adorner
{
  // Be sure to call the base class constructor.
  public SimpleCircleAdorner(UIElement adornedElement)
    : base(adornedElement)
  {
  }

  // A common way to implement an adorner's rendering behavior is to override the OnRender
  // method, which is called by the layout system as part of a rendering pass.
  protected override void OnRender(DrawingContext drawingContext)
  {
    Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

    // Some arbitrary drawing implements.
    SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
    renderBrush.Opacity = 0.2;
    Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
    double renderRadius = 5.0;

    // Draw a circle at each corner.
    drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
    drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
    drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
    drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
  }
}
```



#### 2.1 装饰器如何添加自定义控件？

装饰器没有给出一个直接的方法去添加自定义控件，且装饰器与外界的联系只有一个被装饰的控件，那么我们如何在可视化树上添加自定义控件呢？

我们可以通过间接的方法：

<u>The AddVisualChild method is a notification to the base class that you've acquired a new child into your collection. It doesn't actually add the child to any collection, but it lets the Visual system know that it needs to redraw. This sounds a bit funny, but it's consistent with the way the logical tree is managed as well.</u>

Similarly, when you remove a Visual from your child collection you must call RemoveVisualChild, so the Visual system knows to stop tracking and displaying that Visual. If you'd like, and it sounds appropriate in your scenario, you don't have to use an actual collection for your child visuals. You can simply have a member variable of type Visual, which you can think of as a collection of maximum length 1. Your implementation of GetVisualChild should return the value of that variable, and your implementation of VisualChildrenCount should probably return 0 if that variable is null, or 1 if it's non-null.

通过重写GetVisualChild方法使可视化树重绘

#### 2.2 控件和装饰器如何实现联动效果

如果想实现预期的效果，需要装饰器B和被装饰控件A联动，比如当A发生形变时，B也发生形变

正常的，我们可以用A去通知B,但是装饰器作为独立的一层，不应该和A产生联系

我们可以通过重写ArrangeOverride方法，当重新排序的时候给装饰器内的自定义控件一个自己的大小(位置)

```c#
  public Rect AdonerArrange(Size finalSize)
        {
            int margin = 10;
            return new Rect(-margin, -margin, finalSize.Width + 2 * margin, finalSize.Height + 2 * margin);
        }
```

![ContentAdernor](https://github.com/tiancai4652/MyDecorator/blob/master/MyDecorator/Other/ContentAdernor.gif)

#### 3 平移例子

在自定义控件层，我们通过装饰器拿到被装饰器的控件

自定义装饰器控件-->装饰器层-->被装饰控件

被装饰控件：grid

装饰器层：layer

自定义装饰器控件：contentControl

```c#
 			 var layer = AdornerLayer.GetAdornerLayer(grid);
            DragDropControl contentControl = new DragDropControl(grid);
            contentControl.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.1 };
            layer.Add(new ContentAdorner(grid, contentControl));
```



之后我们在自定义装饰器控件contentControl中操作被装饰控件grid：

```c#
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
```

此时我们通过contentControl控件平移被装饰的控件grid

而contentControl通过AdonerArrange方法跟随grid进行平移

```
 public Rect AdonerArrange(Size finalSize)
        {
            int margin = 10;
            return new Rect(-margin, -margin, finalSize.Width + 2 * margin, finalSize.Height + 2 * margin);

        }
```

![effect2](https://github.com/tiancai4652/MyDecorator/blob/master/MyDecorator/Other/effect2.gif)

***实现了：***

***当控件没有装饰器时，并不具备平移能力。***

***当控件有改装饰器修饰时，具备了平移能力。***

***即常规装饰器可以添加效果，自定义控件装饰器可以添加一些能力。***
