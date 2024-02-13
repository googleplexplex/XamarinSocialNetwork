using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj.Views
{
    public class ClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public FeedPageViewModel view;
        public ClickCommand() { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int likedPostId = (parameter as PostShared).Id;
            Account updatedAccount = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject <List<int>>(updatedAccount.likedPosts);

            if (likedPosts == null) likedPosts = new List<int>();
            if (likedPosts.Contains(likedPostId))
            {
                likedPosts.Remove(likedPostId);
                int dislikedPostIdInViewModel = view.itemsSource.IndexOf(view.itemsSource.First(f => f.Id == likedPostId));
                view.itemsSource[dislikedPostIdInViewModel].likedByUser = new SolidColorBrush(Color.Gray);
            }
            else
            {
                likedPosts.Add(likedPostId);
                int likedPostIdInViewModel = view.itemsSource.IndexOf(view.itemsSource.First(f => f.Id == likedPostId));
                view.itemsSource[likedPostIdInViewModel].likedByUser = new SolidColorBrush(Color.Red);
            }

            updatedAccount.likedPosts = JsonConvert.SerializeObject(likedPosts);
            string a = JsonConvert.SerializeObject(updatedAccount);
            App.Current.Properties["user"] = a;
            App.FriendsTable.UpdateItemAsync(updatedAccount);
            Application.Current.SavePropertiesAsync();
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        public ICommand ClickCommand { get; }
        public FeedPageViewModel sharedPostList = new FeedPageViewModel();

        public FeedPage()
        {
            InitializeComponent();
            ClickCommand = new ClickCommand();
            (ClickCommand as ClickCommand).view = sharedPostList;
        }

        public async void UpdatePage()
        {
            List<PostShared> newItemsSource = new List<PostShared>();

            List<Post> postList = await App.PostsTable.GetItemsAsync();
            Account user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

            for (int i = 0; i < postList.Count(); i++)
            {
                newItemsSource.Add(PostShared.getFromPost(postList[i]));
                newItemsSource[i].autorName = (await App.FriendsTable.GetItemsAsyncById(postList[i].autorId))[0].nickname;
                newItemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
            }

            postsList.ItemsSource = newItemsSource;
        }

        protected override async void OnAppearing()
        {
            if (sharedPostList.itemsSource.Count() == 0)
            {
                List<Post> postList = await App.PostsTable.GetItemsAsync();
                Account user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
                List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

                for (int i = 0; i < postList.Count(); i++)
                {
                    sharedPostList.itemsSource.Add(PostShared.getFromPost(postList[i]));
                    sharedPostList.itemsSource[i].autorName = (await App.FriendsTable.GetItemsAsyncById(postList[i].autorId))[0].nickname;
                    sharedPostList.itemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
                }

                postsList.ItemsSource = sharedPostList.itemsSource;
            }
            else
            {
                UpdatePage();
            }

            base.OnAppearing();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void postsList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            
        }
    }
}