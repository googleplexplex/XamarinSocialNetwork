using System.ComponentModel;
using Xamarin.Forms;
using XamarinNetworkSLN.ViewModels;

namespace XamarinNetworkSLN.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}