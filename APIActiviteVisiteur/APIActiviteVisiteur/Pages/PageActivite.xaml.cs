using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIActiviteVisiteur.ClassesMetier;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APIActiviteVisiteur.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActivite : ContentPage
    {
        public PageActivite()
        {
            InitializeComponent();
        }
        HttpClient ws;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Activite> lesActivites = new List<Activite>();

            ws = new HttpClient();
            var reponse = await ws.GetAsync("http://192.168.1.27:80/API/activites/");
            var content = await reponse.Content.ReadAsStringAsync();
            lesActivites = JsonConvert.DeserializeObject<List<Activite>>(content);
            lvActivites.ItemsSource = lesActivites;
            
        }

        private void lvActivites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void btnAjouter_Clicked(object sender, EventArgs e)
        {

        }
    }
}