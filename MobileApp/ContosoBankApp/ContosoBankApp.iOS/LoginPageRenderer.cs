using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ContosoBankApp.Views;
using ContosoBankApp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace ContosoBankApp.iOS
{
    class LoginPageRenderer : PageRenderer
    {
        LoginPage page;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            page = e.NewElement as LoginPage;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            page.PlatformParameters = new PlatformParameters(this);
        }
    }
}
