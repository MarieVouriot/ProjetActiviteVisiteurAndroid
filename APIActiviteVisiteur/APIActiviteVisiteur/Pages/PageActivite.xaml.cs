using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
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
            // Affichage de activités dans la listView
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
            // Lieu et thème de l'activité sélectionnée s'affiche dans les Entry
            if(lvActivites.SelectedItem != null)
            {
                txtLieuActi.Text = (lvActivites.SelectedItem as Activite).Lieu;
                txtThemeActi.Text = (lvActivites.SelectedItem as Activite).Theme;
            }
        }

        private async void btnModifier_Clicked(object sender, EventArgs e)
        {
            // Vérification qu'un lieu et un thème soient bien entrés dans les entry
            if(txtLieuActi.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Sélectionner une activité", ToastLength.Short).Show();
            }
            else
            {
                if(txtThemeActi.Text == null)
                {
                    Toast.MakeText(Android.App.Application.Context, "Sélectionner une activité", ToastLength.Short).Show();
                }
                else
                {
                    // Modification de l'activité avec les nouvelles données
                    ws = new HttpClient();
                    Activite act = lvActivites.SelectedItem as Activite;
                    act.Lieu = txtLieuActi.Text;
                    act.Theme = txtThemeActi.Text;
                    JObject jact = new JObject()
                    {
                        {"Num", act.Num},
                        {"Date", act.Date},
                        {"Lieu", act.Lieu},
                        {"Theme", act.Theme}
                    };
                    string json = JsonConvert.SerializeObject(jact);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var reponse = await ws.PutAsync("http://192.168.1.27:80/API/activites/", content);
                    
                    // actualisation de la listView
                    List<Activite> lesActivites = new List<Activite>();

                    ws = new HttpClient();
                    reponse = await ws.GetAsync("http://192.168.1.27:80/API/activites/");
                    var flux = await reponse.Content.ReadAsStringAsync();
                    lesActivites = JsonConvert.DeserializeObject<List<Activite>>(flux);
                    lvActivites.ItemsSource = lesActivites;
                }
            }
        }
    }
}