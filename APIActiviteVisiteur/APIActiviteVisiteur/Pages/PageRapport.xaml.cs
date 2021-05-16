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
    public partial class PageRapport : ContentPage
    {
        public PageRapport()
        {
            InitializeComponent();
        }
        HttpClient ws;
        protected override async void OnAppearing()
        {
            // Affichage des rapports dans la listView et remplissage des pickers 
            base.OnAppearing();
            List<Rapport> lesRapports = new List<Rapport>();
            List<Praticien> lesPraticiens = new List<Praticien>();
            List<Visiteur> lesVisiteurs = new List<Visiteur>();

            ws = new HttpClient();
            var reponseRap = await ws.GetAsync("http://192.168.1.27:80/API/DAOActivite.php?id=3");
            var contentRap = await reponseRap.Content.ReadAsStringAsync();
            lesRapports = JsonConvert.DeserializeObject<List<Rapport>>(contentRap);
            lvRapports.ItemsSource = lesRapports;

            ws = new HttpClient();
            var reponse = await ws.GetAsync("http://192.168.1.27:80/API/DAOActivite.php?id=1");
            var content = await reponse.Content.ReadAsStringAsync();
            lesPraticiens = JsonConvert.DeserializeObject<List<Praticien>>(content);
            pckPraticien.ItemsSource = lesPraticiens;

            ws = new HttpClient();
            var reponseVis = await ws.GetAsync("http://192.168.1.27:80/API/DAOActivite.php?id=2");
            var contentVis = await reponseVis.Content.ReadAsStringAsync();
            lesVisiteurs = JsonConvert.DeserializeObject<List<Visiteur>>(contentVis);
            pckVisiteur.ItemsSource = lesVisiteurs;


        }

        private async void btnAjouter_Clicked(object sender, EventArgs e)
        {
            // Vérification que les données soient bien entrés dans les Entry et qu'un praticien et un visiteur soient sélectionnés
            if (dpRapport.Date == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Sélectionner une date", ToastLength.Short).Show();
            }
            else
            {
                if (txtMotifRapport.Text == null)
                {
                    Toast.MakeText(Android.App.Application.Context, "Sélectionner un motif", ToastLength.Short).Show();
                }
                else
                {
                    if(txtBilanRapport.Text == null)
                    {
                        Toast.MakeText(Android.App.Application.Context, "Sélectionner un bilan", ToastLength.Short).Show();
                    }
                    else
                    {
                        if(pckPraticien.SelectedItem == null)
                        {
                            Toast.MakeText(Android.App.Application.Context, "Sélectionner un praticien", ToastLength.Short).Show();
                        }
                        else
                        {
                            if(pckVisiteur.SelectedItem == null)
                            {
                                Toast.MakeText(Android.App.Application.Context, "Sélectionner un visiteur", ToastLength.Short).Show();
                            }else
                            {
                                // Ajout d'un rapport avec les données renseignées
                                ws = new HttpClient();

                                JObject rap = new JObject
                                {
                                    {"Date", dpRapport.Date},
                                    {"Motif", txtMotifRapport.Text },
                                    {"Bilan", txtBilanRapport.Text },
                                    {"PraNum", (pckPraticien.SelectedItem as Praticien).Num },
                                    {"VisMatricule", (pckVisiteur.SelectedItem as Visiteur).Matricule},
                                };
                                string json = JsonConvert.SerializeObject(rap);
                                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                                var reponseRap = await ws.PostAsync("http://192.168.1.27:80/API/activites/", content);
                                
                                // Actualisation de la ListView
                                List<Rapport> lesRapports = new List<Rapport>();

                                ws = new HttpClient();
                                reponseRap = await ws.GetAsync("http://192.168.1.27:80/API/activites/3");
                                var contentRap = await reponseRap.Content.ReadAsStringAsync();
                                lesRapports = JsonConvert.DeserializeObject<List<Rapport>>(contentRap);
                                lvRapports.ItemsSource = lesRapports;
                            }
                        }
                    }
                }
            }
        }

    }
}