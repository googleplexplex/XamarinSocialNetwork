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
        public AllUsersPage()
        {
            InitializeComponent();

            //string[] phones = new string[] { "iPhone 7", "Samsung Galaxy S8", "Huawei P10", "LG G6" };
            //phonesList.ItemsSource = phones;
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateTable();
            // привязка данных
            Account a = new Account();
            a.Id = 0;
            a.nickname = "11";
            a.phone = "12";
            a.desc = "13";
            await App.Database.SaveItemAsync(a);
            phonesList.ItemsSource = await App.Database.GetItemsAsync();

            base.OnAppearing();
        }
        // обработка нажатия элемента в списке
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            return;
        }
    }
}