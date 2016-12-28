using System;
using System.Collections.ObjectModel;
using ContosoBankApp.Models;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using ContosoBankApp.Views;
using System.Windows.Input;
using System.ComponentModel;

namespace ContosoBankApp.ViewModels
{
    public class AccountListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Account> Accounts { get; set; }

        private Command loadAccountsCommand;

        public Command LoadAccountsCommand
        {
            get
            {
                return loadAccountsCommand ?? (loadAccountsCommand = new Command(GetAccountsAsync, () =>
                {
                    return !IsBusy;
                }));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        //Constructor
        public AccountListViewModel()
        {
            Accounts = new ObservableCollection<Account>();

            GetAccountsAsync();
        }


        private async void GetAccountsAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                LoadAccountsCommand.ChangeCanExecute();
                Accounts.Clear();

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
                    IsBusy = false;
                    LoadAccountsCommand.ChangeCanExecute();
                }

            }
            catch (Exception ee)
            {
                //TODO
            }
        }

        public static async void DeleteAccountAsync(string accountNumber)
        {
            try
            {

                var client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.Token);
                var address = $"{App.ApiBaseURL}/api/Accounts/{accountNumber}";

                var response = await client.DeleteAsync(address);

                if (response.IsSuccessStatusCode)
                {
                    //TODO
                }

            }
            catch (Exception ee)
            {
                //TODO
            }
        }
    }
}
