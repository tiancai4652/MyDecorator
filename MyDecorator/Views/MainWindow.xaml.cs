using MyDecorator.Base;
using Prism.Events;
using System.Collections.Generic;
using System.Windows;

namespace MyDecorator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEventAggregator _eventAggregator;
        public MainWindow(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
            _eventAggregator.GetEvent<CollectionChangedEvent>().Subscribe(CollectionChangedEventReceived);
        }

        void CollectionChangedEventReceived(IEnumerable<FrameworkElement> frameworkElements)
        {
            inkCanvas.Children.Clear();
            foreach (var item in frameworkElements)
            {
                inkCanvas.Children.Add(item);
            }
        }
    }
}
