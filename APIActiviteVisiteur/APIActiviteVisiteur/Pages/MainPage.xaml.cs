using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using APIActiviteVisiteur.Pages;

namespace APIActiviteVisiteur
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnActivite_Clicked(object sender, EventArgs e)
        {
            PageActivite page = new PageActivite();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private void btnRapport_Clicked(object sender, EventArgs e)
        {

        }
    }
}
