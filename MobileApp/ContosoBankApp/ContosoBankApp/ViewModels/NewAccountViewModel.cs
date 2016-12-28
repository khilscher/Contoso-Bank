using System;
using System.Collections.ObjectModel;
using ContosoBankApp.Models;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using ContosoBankApp.Views;
using System.Net.Http;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;

namespace ContosoBankApp.ViewModels
{
    public class NewAccountViewModel : INotifyPropertyChanged
    {

        string _displayText = "";
        string _accountNumber = "";
        string _accountType = "";
        string _accountBalance = "";

        public string AccountNumber
        {
            set
            {
                if (_accountNumber != value)
                {
                    _accountNumber = value;
                    OnPropertyChanged("AccountNumber");
                }
            }
            get { return _accountNumber; }
        }

        public string AccountType
        {
            set
            {
                if (_accountType != value)
                {
                    _accountType = value;
                    OnPropertyChanged("AccountType");
                }
            }
            get { return _accountType; }
        }

        public string AccountBalance
        {
            set
            {
                if (_accountBalance != value)
                {
                    _accountBalance= value;
                    OnPropertyChanged("AccountBalance");
                }
            }
            get { return _accountBalance; }
        }

        public string CreateAccountStatus
        {
            protected set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged("CreateAccountStatus");
                }
            }
            get { return _displayText; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNewAcctCmd { protected set; get; }

        public NewAccountViewModel()
        {
            this.CreateNewAcctCmd = new Command((nothing) =>
            {
                this.CreateAccountAsync();
            });

        }

        private async void CreateAccountAsync()
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
              
                Account newAccount = new Account();
                newAccount.AccountNumber = Convert.ToInt32(_accountNumber);
                newAccount.AccountType = _accountType;
                newAccount.AccountBalance = Convert.ToDouble(_accountBalance);
                
                string newAccountJson = JsonConvert.SerializeObject(newAccount);
                var content = new StringContent(newAccountJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(address, content);

                if (response.IsSuccessStatusCode)
                {
                    CreateAccountStatus = "Account created successfully";
                    await Task.Delay(3000);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    CreateAccountStatus = response.StatusCode.ToString();
                }


            }
            catch (Exception ee)
            {
                //TODO
            }
        }


    }
}
    

