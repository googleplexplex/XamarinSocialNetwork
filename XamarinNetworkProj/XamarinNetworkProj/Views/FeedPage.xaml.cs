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
        public ClickCommand() { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public byte[] AddByteToArray(byte[] bArray, byte[] newBytes)
        {
            byte[] newArray = new byte[bArray.Length + newBytes.Length];
            bArray.CopyTo(newArray, 0);

            for(int i = 0; i < newBytes.Length; i++)
            {
                newArray[bArray.Length + i] = newBytes[i];
            }

            return newArray;
        }
        public void Execute(object parameter)
        {
            Account updatedAccount = JsonConvert.DeserializeObject<Account>(App.Current.Properties["user"] as string);
            updatedAccount.likedPosts = AddByteToArray(updatedAccount.likedPosts, BitConverter.GetBytes((parameter as PostShared).Id));

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

        public FeedPage()
        {
            InitializeComponent();
            ClickCommand = new ClickCommand();
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