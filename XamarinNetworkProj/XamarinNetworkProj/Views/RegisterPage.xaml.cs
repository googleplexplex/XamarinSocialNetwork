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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegButton_Clicked(object sender, EventArgs e)
        {
            if((RegLogin.Text.Length > 4 && RegLogin.Text.Length < 20) &&
                (RegPassowrd.Text.Length > 4 && RegPassowrd.Text.Length < 20) &&
                RegPassowrd.Text == RegSecond.Text)
            {
                int lastId = App.FriendsTable.database.Table<Account>().OrderBy(f => f.Id).ToListAsync().Result.Last().Id;
                Account newAcc = App.AccountConstr(RegLogin.Text, RegPassowrd.Text, RegDesc.Text);
                newAcc.Id = lastId + 1;
                App.FriendsTable.InsertItemAsync(newAcc);
                App.Current.Properties.Add("user", JsonConvert.SerializeObject(newAcc));
                Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new MainPage();
            }
        }

        private void RegLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(RegLogin.Text.Length > 4 && RegLogin.Text.Length < 20))
            {
                RegLogin.TextColor = Color.IndianRed;
            }
            else
            {
                RegLogin.TextColor = Color.Default;
            }
        }

        private void RegPassowrd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(RegPassowrd.Text.Length > 4 && RegPassowrd.Text.Length < 20))
                RegPassowrd.TextColor = Color.IndianRed;
            else
                RegPassowrd.TextColor = Color.Default;
        }

        private void RegSecond_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegPassowrd.Text != RegSecond.Text)
                RegSecond.TextColor = Color.IndianRed;
            else
                RegSecond.TextColor = Color.Default;
        }
    }
}