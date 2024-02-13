using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinNetworkProj.Model
{
    public class FeedPageViewModel : INotifyPropertyChanged
    {
        public List<PostShared> _itemsSource = new List<PostShared>();
        public List<PostShared> itemsSource
        {
            get { return _itemsSource; }
            set
            {
                if (_itemsSource != value)
                {
                    _itemsSource = value;
                    OnPropertyChanged("itemsSource");
                }
            }
        }


        public FeedPageViewModel() : base() { }
        public FeedPageViewModel(List<PostShared> itemsSource) : base()
        {
            this.itemsSource = itemsSource;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
