using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ContosoBankApp.Models;
using Newtonsoft.Json;

namespace ContosoBankApp
{
    public partial class AccountListPage : ContentPage
    {

        public AccountListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            ObservableCollection<Account> accounts = await GetAccountListAsync();

            if (accounts.Count > 0)
            {
                lstView.ItemsSource = accounts;
            }
        }
        

        private async Task<ObservableCollection<Account>> GetAccountListAsync()
        {
            ObservableCollection<Account> acctList = new ObservableCollection<Account>();

            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainPage.idToken);
            var address = $"https://<yoururl>.azurewebsites.net/api/Accounts/";

            var response = await client.GetAsync(address);

            if (response.IsSuccessStatusCode)
            {
                var airportJson = response.Content.ReadAsStringAsync().Result;

                acctList = JsonConvert.DeserializeObject<ObservableCollection<Account>>(airportJson);
            }
            return acctList;

        }
    }
}
