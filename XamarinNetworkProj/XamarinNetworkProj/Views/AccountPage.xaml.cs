using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public Account viewedUserId;
        public ICommand ClickCommand { get; }
        public FeedPageViewModel sharedPostList = new FeedPageViewModel();

        public AccountPage(Account user)
        {
            viewedUserId = user;

            InitializeComponent();

            ClickCommand = new ClickCommand();
            (ClickCommand as ClickCommand).view = sharedPostList;
            (ClickCommand as ClickCommand).updateView += UpdatePage;

            NicknameLabel.BindingContext = viewedUserId;
            NicknameLabel.SetBinding(Label.TextProperty, "nickname");
            DescLabel.BindingContext = viewedUserId;
            DescLabel.SetBinding(Label.TextProperty, "desc");
        }

        public async void UpdatePage(object sender = null, EventArgs a = null)
        {
            List<PostShared> newItemsSource = new List<PostShared>();

            List<Post> postList = await App.PostsTable.GetItemsAsyncById(viewedUserId.Id);
            Account user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

            for (int i = 0; i < postList.Count(); i++)
            {
                newItemsSource.Add(PostShared.getFromPost(postList[i]));
                newItemsSource[i].autorName = viewedUserId.nickname;
                newItemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
            }

            postsList.ItemsSource = newItemsSource;
        }

        protected override async void OnAppearing()
        {
            if (sharedPostList.itemsSource.Count() != 0)
            {
                UpdatePage();
            }
            else
            {
                List<Post> postList = await App.PostsTable.GetItemsAsyncById(viewedUserId.Id);
                Account user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
                List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

                for (int i = 0; i < postList.Count(); i++)
                {
                    sharedPostList.itemsSource.Add(PostShared.getFromPost(postList[i]));
                    sharedPostList.itemsSource[i].autorName = viewedUserId.nickname;
                    sharedPostList.itemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
                }

                postsList.ItemsSource = sharedPostList.itemsSource;
            }

            base.OnAppearing();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}