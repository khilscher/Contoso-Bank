using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ContosoBankApp.Views;

namespace ContosoBankApp.ViewModels
{
    public class ListPageViewModel
    {

        public string UserName { get; set; }

        public Command ShowAuthenticationInfo
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AuthenticationInfoPage());
                });
            }
        }

        public Command ShowAccountList
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AccountListPage());
                });
            }
        }

        //Constructor
        public ListPageViewModel()
        {
            UserName = $"Welcome {App.UserName}";

        }
    }
}
