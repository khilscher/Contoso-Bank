using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using ContosoBankApp.Views;
using ContosoBankApp.ViewModels;
using System.Collections.ObjectModel;

namespace ContosoBankApp.Views
{
    public partial class MainPage : ContentPage
    {
        public string UserName { get; set; }
        public static ObservableCollection<string> items { get; set; }
        public IPlatformParameters platformParameters { get; set; }

        public MainPage()
        {
            items = new ObservableCollection<string>() { "View Accounts", "Open New Account", "Your Profile", "Logout" };

            InitializeComponent();

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            switch (e.SelectedItem.ToString())
            {
                case "View Accounts":
                    App.Current.MainPage.Navigation.PushAsync(new AccountListPage());
                    break;
                case "Open New Account":
                    App.Current.MainPage.Navigation.PushAsync(new NewAccountPage());
                    break;
                case "Your Profile":
                    App.Current.MainPage.Navigation.PushAsync(new AuthenticationInfoPage());
                    break;
                case "Logout":
                    Logout();
                    break;
            }

            //comment out if you want to keep selections
            ListView lst = (ListView)sender;
            lst.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                App.AuthenticationClient = new PublicClientApplication(App.ClientId);
                AuthenticationResult ar = await App.AuthenticationClient.AcquireTokenSilentAsync(App.Scopes,
                 string.Empty,
                 App.Authority,
                 App.SignUpSignInPolicy,
                 false);

                App.Token = ar.Token;
                App.TokenExpiresOn = ar.ExpiresOn.ToString();

            }
            catch (Exception ee)
            {
                DisplayAlert("An error has occurred", "Exception message: " + ee.Message, "Dismiss");
                App.AuthenticationClient = new PublicClientApplication(App.ClientId);
                App.AuthenticationClient.UserTokenCache.Clear(App.AuthenticationClient.ClientId);
                Navigation.PushAsync(new LoginPage());
            }
        }

        public async void Logout()
        {
            try
            {

                foreach (var user in App.AuthenticationClient.Users)
                {
                    user.SignOut();
                }

                //Reset the main page from the MainPage to the LoginPage
                App.Current.MainPage = new NavigationPage(new LoginPage());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
