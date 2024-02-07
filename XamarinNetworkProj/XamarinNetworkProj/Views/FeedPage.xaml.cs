using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            postsList.ItemsSource = await App.PostsTable.GetItemsAsync();

            base.OnAppearing();
        }
        // обработка нажатия элемента в списке
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            return;
        }
    }
}