using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ContosoBankApp.ViewModels;

namespace ContosoBankApp.Views
{
    public partial class AuthenticationInfoPage : ContentPage
    {
        public AuthenticationInfoPage()
        {
            InitializeComponent();

            //Bind to ViewModel for entire Content Page. Binding is inherited by all children on the page
            BindingContext = new AuthenticationInfoViewModel();
        }
    }
}
