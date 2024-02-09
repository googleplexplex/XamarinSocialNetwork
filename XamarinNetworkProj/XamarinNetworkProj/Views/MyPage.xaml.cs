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
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }

        private void quitButton_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("user");
            Application.Current.SavePropertiesAsync();
            Navigation.PushAsync(new NavigationPage(new LoginPage()));
        }
    }
}