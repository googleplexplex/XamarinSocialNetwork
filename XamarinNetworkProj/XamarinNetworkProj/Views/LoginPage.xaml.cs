using Newtonsoft.Json;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            tapGesture.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new RegisterPage());
            };
            SignUpHyperlink.GestureRecognizers.Add(tapGesture);

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            Account back = await App.FriendsTable.GetItemAsyncByLP(InpLogin.Text, InpPass.Text);
            if(back == null)
            {
                InpPass.TextColor = Color.IndianRed;
            }
            else
            {
                App.Current.Properties.Add("user", JsonConvert.SerializeObject(back));
                Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new MainPage();
            }
        }

        private void InpPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            InpPass.TextColor = Color.Default;
        }
    }
}