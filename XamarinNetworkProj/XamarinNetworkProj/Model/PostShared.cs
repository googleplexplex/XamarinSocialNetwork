using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace XamarinNetworkProj.Model
{
    public class PostShared : Post, INotifyPropertyChanged
    {
        public string autorName { get; set; }
        private Brush _likedByUser;
        public Brush likedByUser
        {
            get { return _likedByUser; }
            set
            {
                if (_likedByUser != value)
                {
                    _likedByUser = value;
                    OnPropertyChanged("likedByUser");
                }
            }
        }

        public PostShared() : base() { }
        public PostShared(string autorName, int autorId, string content, int likes, DateTime postedOn, int id = 0) : base(autorId, content, likes, postedOn, id)
        {
            this.autorName = autorName;
        }

        static public PostShared getFromPost(Post p)
        {
            return new PostShared("", p.autorId, p.content, p.likes, p.postedOn, p.Id);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
