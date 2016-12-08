using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace ContosoBankApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnLogin.IsEnabled = false;
                var authenticationResult = await App.AuthenticationClient.AcquireTokenAsync(App.Scopes,
                    "",
                    UiOptions.SelectAccount,
                    string.Empty,
                    null,
                    App.Authority,
                    App.SignUpSignInPolicy);

                App.Current.MainPage = new NavigationPage(new MainPage(authenticationResult));
                //await Navigation.PushAsync(new MainPage);

                //await Navigation.PushAsync(new AuthenticationSuccessfulPage(authenticationResult));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Authenticating", ex.Message, "OK");
                btnLogin.IsEnabled = true;
            }
        }
    }
}
