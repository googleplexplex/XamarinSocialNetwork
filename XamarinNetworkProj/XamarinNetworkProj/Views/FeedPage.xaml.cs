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
    public class ClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public FeedPage view;
        public ClickCommand() { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Account updatedAccount = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject <List<int>>(updatedAccount.likedPosts);

            if (likedPosts == null) likedPosts = new List<int>();
            if(likedPosts.Contains((parameter as PostShared).Id))
            {
                likedPosts.Remove((parameter as PostShared).Id);
            }
            likedPosts.Add((parameter as PostShared).Id);
            updatedAccount.likedPosts = JsonConvert.SerializeObject(likedPosts.GroupBy(x => x).Select(x => x.First()).ToList());

            string a = JsonConvert.SerializeObject(updatedAccount);
            App.Current.Properties["user"] = a;
            App.FriendsTable.UpdateItemAsync(updatedAccount);
            Application.Current.SavePropertiesAsync();
            return;
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        public ICommand ClickCommand { get; }
        public List<PostShared> sharedPostList = new List<PostShared>();

        public FeedPage()
        {
            InitializeComponent();
            ClickCommand = new ClickCommand();
            (ClickCommand as ClickCommand).view = this;
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.PostsTable.CreateTable();

            List<Post> postList = await App.PostsTable.GetItemsAsync();
            Account user = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject<List<int>>(user.likedPosts);

            for (int i = 0; i < postList.Count(); i++)
            {
                sharedPostList.Add(PostShared.getFromPost(postList[i]));
                sharedPostList[i].autorName = (await App.FriendsTable.GetItemsAsyncById(postList[i].autorId))[0].nickname;
                sharedPostList[i].likedByUser = likedPosts.Contains(postList[i].Id) ? "icon_liked.png" : "icon_tolike.png";
            }

            postsList.ItemsSource = sharedPostList;

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