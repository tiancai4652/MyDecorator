using MyDecorator.Base;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            Controls.Add(new Button() { Width = 200, Height = 200 });
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

    }
}
