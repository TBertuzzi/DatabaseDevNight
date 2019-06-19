using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediumRSS.Models;
using MediumRSS.ViewModels;
using Xamarin.Forms;

namespace MediumRSS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainViewModel();
        }

        void ListView_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var selectedItem = (RssData)e.Item;
            Navigation.PushAsync(new RSSWebViewPage(selectedItem.Link));
        }
    }
}
