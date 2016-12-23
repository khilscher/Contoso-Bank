using System;
using System.Collections.ObjectModel;
using ContosoBankApp.Models;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using ContosoBankApp.Views;

// References
// https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/data-and-databinding/#Data_Binding
// http://www.davidbritch.com/2015/05/creating-xamarinforms-app-that-uses.html

namespace ContosoBankApp.ViewModels
{
    public class AccountListViewModel
    {

        public ObservableCollection<Account> Accounts { get; set; }
        Page _page;

        //Constructor
        public AccountListViewModel()
        {
            MessagingCenter.Subscribe<Page>(this, "BindingContextChanged.AccountListViewModel", (sender) =>
            {
                _page = sender;
            });

            Accounts = new ObservableCollection<Account>();
            GetAccountsAsync();
        }

        private async void GetAccountsAsync()
        {
            try
            {
                AuthenticationResult ar = await App.AuthenticationClient.AcquireTokenSilentAsync(App.Scopes,
                 string.Empty,
                 App.Authority,
                 App.SignUpSignInPolicy,
                 false);

                App.Token = ar.Token;
                App.TokenExpiresOn = ar.ExpiresOn.ToString();

                var client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.Token);
                var address = $"{App.ApiBaseURL}/api/Accounts/";

                var response = await client.GetAsync(address);

                if (response.IsSuccessStatusCode)
                {
                    var airportJson = response.Content.ReadAsStringAsync().Result;

                    ObservableCollection<Account> acctList = new ObservableCollection<Account>();

                    acctList = JsonConvert.DeserializeObject<ObservableCollection<Account>>(airportJson);

                    foreach (var acct in acctList)
                    {
                        Accounts.Add(acct);
                    }
                }

            }
            catch (Exception ee)
            {
                await _page.DisplayAlert("An error has occurred", "Exception message: " + ee.Message, "Dismiss");
                App.AuthenticationClient.UserTokenCache.Clear(App.AuthenticationClient.ClientId);
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        }
    }
}
