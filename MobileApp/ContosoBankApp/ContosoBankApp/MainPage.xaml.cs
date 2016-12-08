using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace ContosoBankApp
{
    public partial class MainPage : ContentPage
    {
        public static string UserName { get; set; }
        public static string idToken { get; set; }
        private AuthenticationResult authenticationInfo;

        public MainPage(AuthenticationResult authenticationResult)
        {
            InitializeComponent();

            authenticationInfo = authenticationResult;

            UserName = $"Welcome {authenticationResult.User.Name}";
            idToken = authenticationResult.IdToken;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await App.AuthenticationClient.AcquireTokenSilentAsync(App.Scopes,
             string.Empty,
             App.Authority,
             App.SignUpSignInPolicy,
             false);
        }

        public async void ViewAccountListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountListPage());
        }

        public async void ViewLoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthenticationInfoPage(authenticationInfo));
        }
    }
}
