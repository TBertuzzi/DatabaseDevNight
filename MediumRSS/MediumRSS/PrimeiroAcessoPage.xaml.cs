using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MediumRSS
{
    public partial class PrimeiroAcessoPage : ContentPage
    {
        public void Handle_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("PrimeiroAcesso", false);

            Navigation.PushAsync(new MainPage());
        }

        public PrimeiroAcessoPage()
        {
            InitializeComponent();

            
        }
    }
}
