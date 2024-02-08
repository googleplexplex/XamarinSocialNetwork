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
    public partial class AccountPage : ContentPage
    {
        public Account viewedUserId;

        public AccountPage(Account user)
        {
            viewedUserId = user;

            InitializeComponent();

            NicknameLabel.BindingContext = viewedUserId;
            NicknameLabel.SetBinding(Label.TextProperty, "nickname");
            DescLabel.BindingContext = viewedUserId;
            DescLabel.SetBinding(Label.TextProperty, "desc");
        }

        protected override async void OnAppearing()
        {
            List<PostShared> sharedPostList = new List<PostShared>();
            List<Post> postList = await App.PostsTable.GetItemsAsyncById(viewedUserId.Id);
            for(int i = 0; i < postList.Count(); i++)
            {
                sharedPostList.Add(PostShared.getFromPost(postList[i]));
                sharedPostList[i].autorName = viewedUserId.nickname;
            }

            postsList.ItemsSource = sharedPostList;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}