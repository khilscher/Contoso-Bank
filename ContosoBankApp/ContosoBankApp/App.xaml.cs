using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace ContosoBankApp
{
    public partial class App : Application
    {
        //Code Sample from https://blog.xamarin.com/authenticating-mobile-apps-with-azure-active-directory-b2c/

        // TODO: Add Azure AD B2C tenant information.
        public static string ClientId = "6abd2e3d-af2d-4180-a46c-a26cfb88c1b0";
        public static string SignUpSignInPolicy = "B2C_1_signin_signup";
        public static string[] Scopes = { ClientId };
        public static string Authority = "https://login.microsoftonline.com/testb2c100.onmicrosoft.com/";

        public App()
        {
            InitializeComponent();

            AuthenticationClient = new PublicClientApplication(ClientId);

            MainPage = new NavigationPage(new Login());
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
