using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace ContosoBankApp
{
    public partial class AuthenticationSuccessfulPage : ContentPage
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayId { get; set; }
        public string ExpiresOn { get; set; }
        public string User { get; set; }
        public string TokenType { get; set; }
        public string IdToken { get; set; }
        public string TenantId { get; set; }
        public string AccessToken { get; set; }
        public string UClientId { get; set; }

        public AuthenticationSuccessfulPage(AuthenticationResult authenticationResult)
        {
            InitializeComponent();

            UserId = $"User Id: {authenticationResult.User.UniqueId}";
            User = $"User: {authenticationResult.User.ToString()}";
            UserName = $"UserName: {authenticationResult.User.Name}";
            DisplayId = $"UserName: {authenticationResult.User.DisplayableId}";
            ExpiresOn = $"Token Expires: {authenticationResult.ExpiresOn.ToString()}";
            UClientId = $"User ClientId: {authenticationResult.User.ClientId}";
            TokenType = $"Token Type: {authenticationResult.TokenType}";

            //JWT Token https://docs.microsoft.com/en-us/azure/active-directory/active-directory-protocols-oauth-code
            IdToken = $"Id_Token: {authenticationResult.IdToken}";

            AccessToken = $"Access Token: {authenticationResult.Token}";
            TenantId = $"Tenant ID: {authenticationResult.TenantId}";

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
    }
}
