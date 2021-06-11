using MyDecorator.Base;
using MyDecorator.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MyDecorator.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        private string _title = "Decorator Test Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        IEventAggregator _eventAggregator;
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Controls.CollectionChanged += Controls_CollectionChanged;
        }

        private void Controls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _eventAggregator.GetEvent<CollectionChangedEvent>().Publish(Controls);
        }

        private ObservableCollection<FrameworkElement> _Controls = new ObservableCollection<FrameworkElement>(){};
        public ObservableCollection<FrameworkElement> Controls
        {
            get { return _Controls; }
            set { SetProperty(ref _Controls, value); }
        }

        private DelegateCommand _AddControlCommand;
        public DelegateCommand AddControlCommand =>
            _AddControlCommand ?? (_AddControlCommand = new DelegateCommand(ExecuteAddControlCommand));

        Grid grid;
        void ExecuteAddControlCommand()
        {
            grid = new Grid() { Width = 200, Height = 200,Background= new SolidColorBrush( Colors.Gray),Opacity=0.5 };
            InkCanvas.SetTop(grid,200);
            InkCanvas.SetLeft(grid, 400);

            Controls.Add(grid);
       
        }

        private DelegateCommand _AddContentAdornerCommand;
        public DelegateCommand AddContentAdornerCommand =>
            _AddContentAdornerCommand ?? (_AddContentAdornerCommand = new DelegateCommand(ExecuteAddContentAdornerCommand));

        void ExecuteAddContentAdornerCommand()
        {
            var layer = AdornerLayer.GetAdornerLayer(grid);
            DragDropControl contentControl = new DragDropControl(grid);
            contentControl.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.1 };
            layer.Add(new ContentAdorner(grid, contentControl));

        }

        private DelegateCommand _AddCommonAdornerCommand;
        public DelegateCommand AddCommonAdornerCommand =>
            _AddCommonAdornerCommand ?? (_AddCommonAdornerCommand = new DelegateCommand(ExecuteAddCommonAdornerCommand));

        void ExecuteAddCommonAdornerCommand()
        {
            var layer = AdornerLayer.GetAdornerLayer(grid);
            layer.Add(new SimpleCircleAdorner(grid));
        }

        private DelegateCommand _RemoveAdornerCommand;
        public DelegateCommand RemoveAdornerCommand =>
            _RemoveAdornerCommand ?? (_RemoveAdornerCommand = new DelegateCommand(ExecuteRemoveAdornerCommand));

        void ExecuteRemoveAdornerCommand()
        {
            var layer = AdornerLayer.GetAdornerLayer(grid);
            var arr = layer.GetAdorners(grid);
            if (arr != null&&arr.Length>0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    layer.Remove(arr[i]);
                }
            }
        }
    }
}
