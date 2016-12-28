using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoBankApp.Models;
using ContosoBankApp.ViewModels;
using Xamarin.Forms;

namespace ContosoBankApp.Views
{
    public partial class NewAccountPage : ContentPage
    {
        public NewAccountPage()
        {
            InitializeComponent();

            //Bind to ViewModel for entire Content Page. Binding is inherited by all children on the page
            BindingContext = new NewAccountViewModel();

        }
    }
}
