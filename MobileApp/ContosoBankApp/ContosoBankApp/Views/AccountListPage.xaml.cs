using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ContosoBankApp.Models;
using ContosoBankApp.ViewModels;
using Newtonsoft.Json;
using Microsoft.Identity.Client;

namespace ContosoBankApp.Views
{
    public partial class AccountListPage : ContentPage
    {

        public AccountListPage()
        {
            InitializeComponent();

            //Bind to ViewModel for entire Content Page. Binding is inherited by all children on the page
            BindingContext = new AccountListViewModel();

        }

        //TODO Update this to MVVM pattern
        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            AccountListViewModel.DeleteAccountAsync(mi.CommandParameter.ToString());
            DisplayAlert("Delete", "Account number " + mi.CommandParameter + " deleted", "OK");

        }
        
    }
}
