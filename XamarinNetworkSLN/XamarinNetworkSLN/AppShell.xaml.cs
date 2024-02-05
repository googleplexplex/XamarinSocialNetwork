using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinNetworkSLN.ViewModels;
using XamarinNetworkSLN.Views;

namespace XamarinNetworkSLN
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
