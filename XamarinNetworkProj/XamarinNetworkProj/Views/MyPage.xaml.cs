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
    public partial class MyPage : ContentPage
    {
        public Account user;
        public ICommand ClickCommand { get; }
        public FeedPageViewModel sharedPostList = new FeedPageViewModel();

        public MyPage()
        {
            user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);

            InitializeComponent();
            ClickCommand = new ClickCommand();
            (ClickCommand as ClickCommand).view = sharedPostList;
            (ClickCommand as ClickCommand).updateView += UpdatePage;

            NicknameLabel.Text = user.nickname;
            DescLabel.Text = user.desc;
        }

        public async void UpdatePage(object sender = null, EventArgs a = null)
        {
            user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<Post> postList = await App.PostsTable.GetItemsAsyncById(user.Id);
            List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

            List<PostShared> newItemsSource = new List<PostShared>();

            for (int i = 0; i < postList.Count(); i++)
            {
                newItemsSource.Add(PostShared.getFromPost(postList[i]));
                newItemsSource[i].autorName = user.nickname;
                newItemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
            }

            postsList.ItemsSource = newItemsSource;
        }

        protected override async void OnAppearing()
        {
            user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<Post> postList = await App.PostsTable.GetItemsAsyncById(user.Id);
            List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

            if (sharedPostList.itemsSource.Count() != 0)
            {
                UpdatePage();
            }
            else
            {
                for (int i = 0; i < postList.Count(); i++)
                {
                    sharedPostList.itemsSource.Add(PostShared.getFromPost(postList[i]));
                    sharedPostList.itemsSource[i].autorName = user.nickname;
                    sharedPostList.itemsSource[i].likedByUser = likedPosts.Contains(postList[i].Id) ? new SolidColorBrush(Color.Red) : new SolidColorBrush(Color.Gray);
                }
                postsList.ItemsSource = sharedPostList.itemsSource;
            }

            base.OnAppearing();
        }

        private async void quitButton_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("user");
            await Application.Current.SavePropertiesAsync();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void NewPostButton_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Новый пост", "", "OK", "Cancel", "What would you like to say to the world?", keyboard:Keyboard.Default);

            if(result != null)
            {
                Post addedPost = new Post(user.Id, result, 0);
                await App.PostsTable.InsertItemAsync(addedPost);
                PostShared addedPostShared = PostShared.getFromPost(addedPost);
                addedPostShared.autorName = user.nickname;
                addedPostShared.likedByUser = new SolidColorBrush(Color.Red);
                UpdatePage();
            }
        }
    }
}