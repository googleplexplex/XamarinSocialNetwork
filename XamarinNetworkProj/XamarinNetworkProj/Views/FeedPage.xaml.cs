﻿using Newtonsoft.Json;
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
        public EventHandler updateView = null;
        public ClickCommand() { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PostShared likedPost = (parameter as PostShared);
            int likedPostId = likedPost.Id;
            Account updatedAccount = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            List<int> likedPosts = JsonConvert.DeserializeObject <List<int>>(updatedAccount.likedPosts);

            if (likedPosts == null) likedPosts = new List<int>();
            if (likedPosts.Contains(likedPostId))
            {
                likedPosts.Remove(likedPostId);
                int dislikedPostIdInViewModel = view.itemsSource.IndexOf(view.itemsSource.First(f => f.Id == likedPostId));
                likedPost.likes--;
                view.itemsSource[dislikedPostIdInViewModel].likedByUser = new SolidColorBrush(Color.Gray);
            }
            else
            {
                likedPosts.Add(likedPostId);
                int likedPostIdInViewModel = view.itemsSource.IndexOf(view.itemsSource.First(f => f.Id == likedPostId));
                likedPost.likes++;
                view.itemsSource[likedPostIdInViewModel].likedByUser = new SolidColorBrush(Color.Red);
            }
            App.PostsTable.UpdateItemAsync(new Post(likedPost.autorId, likedPost.content, likedPost.likes, likedPost.Id));

            updatedAccount.likedPosts = JsonConvert.SerializeObject(likedPosts);
            App.Current.Properties["user"] = JsonConvert.SerializeObject(updatedAccount);
            App.FriendsTable.UpdateItemAsync(updatedAccount);
            Application.Current.SavePropertiesAsync();

            updateView?.Invoke(null, null);
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
            (ClickCommand as ClickCommand).updateView += UpdatePage;
        }

        public async void UpdatePage(object sender = null, EventArgs a = null)
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