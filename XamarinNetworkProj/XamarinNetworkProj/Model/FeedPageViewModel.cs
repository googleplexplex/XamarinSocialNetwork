using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinNetworkProj.Model
{
    public class FeedPageViewModel : INotifyPropertyChanged
    {
        public List<PostShared> itemsSource = new List<PostShared>();


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
