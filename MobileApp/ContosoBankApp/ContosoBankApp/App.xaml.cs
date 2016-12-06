using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Xamarin.Forms;

//Code Sample from https://blog.xamarin.com/authenticating-mobile-apps-with-azure-active-directory-b2c/

namespace ContosoBankApp
{
    public partial class App : Application
    {

        public static PublicClientApplication AuthenticationClient { get; set; }

        // TODO: Add your Azure AD B2C tenant information.
        public static string ClientId = ""; 
        public static string SignUpSignInPolicy = "";
        public static string[] Scopes = { ClientId };
        public static string Authority = "";

        public App()
        {
            InitializeComponent();

            AuthenticationClient = new PublicClientApplication(ClientId);

            //MainPage = new ContosoBankApp.MainPage();

            MainPage = new NavigationPage(new LoginPage());
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
