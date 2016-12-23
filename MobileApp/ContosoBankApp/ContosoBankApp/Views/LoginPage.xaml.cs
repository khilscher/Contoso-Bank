using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;


namespace ContosoBankApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            // let's see if we have a user in our cache
            try
            {
                AuthenticationResult ar = await App.AuthenticationClient.AcquireTokenSilentAsync(App.Scopes,
                     string.Empty,
                     App.Authority,
                     App.SignUpSignInPolicy,
                     false);

                App.UserId = ar.User.UniqueId;
                App.UserName = ar.User.Name;
                App.Token = ar.Token;
                App.TokenExpiresOn = ar.ExpiresOn.ToString();
                App.TokenType = ar.TokenType;

                App.Current.MainPage = new NavigationPage(new MainPage());

            }
            catch
            {
                // doesn't matter, we go into interactive login mode
            }
        }

        public async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnLogin.IsEnabled = false;
                AuthenticationResult ar = await App.AuthenticationClient.AcquireTokenAsync(App.Scopes,
                    "",
                    UiOptions.SelectAccount,
                    string.Empty,
                    null,
                    App.Authority,
                    App.SignUpSignInPolicy);

                App.UserId = ar.User.UniqueId;
                App.UserName = ar.User.Name;
                App.Token = ar.Token;
                App.TokenExpiresOn = ar.ExpiresOn.ToString();
                App.TokenType = ar.TokenType;

                //Reset the main page from the login page to MainPage
                App.Current.MainPage = new NavigationPage(new MainPage());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Authenticating", ex.Message, "OK");
                btnLogin.IsEnabled = true;
            }
        }
    }
}
