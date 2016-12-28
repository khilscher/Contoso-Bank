using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using ContosoBankApp.Views;
using Xamarin.Forms;

//Code Sample from https://blog.xamarin.com/authenticating-mobile-apps-with-azure-active-directory-b2c/

namespace ContosoBankApp
{
    public partial class App : Application
    {

        public static PublicClientApplication AuthenticationClient { get; set; }
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string Token { get; set; }
        public static string TokenExpiresOn { get; set; }
        public static string TokenType { get; set; }

        // TODO: Add your Azure AD B2C tenant information.
        public static string ClientId = "<client ID of application registered in AAD B2C";
        public static string SignUpSignInPolicy = "B2C_1_signin_signup";
        public static string[] Scopes = { ClientId };
        public static string Authority = "https://login.microsoftonline.com/<tenant>.onmicrosoft.com/";
        public static string ApiBaseURL = "https://<apiurl>.azurewebsites.net";

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
