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
    public partial class AllUsersPage : ContentPage
    {
        List<Account> accountsSource;

        public AllUsersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.FriendsTable.CreateTable();
            accountsSource = await App.FriendsTable.GetItemsAsync();
            phonesList.ItemsSource = accountsSource;

            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            if (e.SelectedItemIndex != -1)
            {
                var selectedUser = accountsSource[e.SelectedItemIndex];
                await Navigation.PushAsync(new AccountPage(selectedUser));
            }
        }
    }
}