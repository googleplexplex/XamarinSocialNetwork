using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        public FeedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.PostsTable.CreateTable();

            List<PostShared> sharedPostList = new List<PostShared>();
            List<Post> postList = await App.PostsTable.GetItemsAsync();
            for (int i = 0; i < postList.Count(); i++)
            {
                sharedPostList.Add(PostShared.getFromPost(postList[i]));
                sharedPostList[i].autorName = (await App.FriendsTable.GetItemsAsyncById(postList[i].autorId))[0].nickname;
            }

            postsList.ItemsSource = sharedPostList;

            base.OnAppearing();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}