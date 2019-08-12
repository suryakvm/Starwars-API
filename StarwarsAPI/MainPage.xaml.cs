using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;


namespace StarwarsAPI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
                
            var client = new GraphQLClient("https://swapi.apis.guru/"); 

            client.DefaultRequestHeaders.Add("User-Agent", " GraphQLApp");

            GraphQLRequest graphQLRequest = new GraphQLRequest
            {
                Query = "query{ allFilms { films { title director} }  }"
            };

            try
            {
                GraphQLResponse response = await client.PostAsync(graphQLRequest);
                List<Film> films = new List<Film>();
                foreach (Newtonsoft.Json.Linq.JObject f in response.Data.allFilms.films)
                {
                    Film film = f.ToObject<Film>();
                    films.Add(film);

                }
                film_list.ItemsSource = films;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine((string)ex.Message + " :: " + ex.InnerException.Message);
            }

            }
    }
}
