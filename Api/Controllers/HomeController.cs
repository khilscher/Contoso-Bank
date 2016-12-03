using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class HomeController : Controller
    {

        static HttpClient httpClient = new HttpClient();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        
        public ActionResult Accounts()
        {
            //AccountsController accounts = new AccountsController();
            //List<Account> accountList = accounts.Get();
            //ViewBag.Title = "Accounts Page";
            //ViewBag.Accounts = accountList;

            return View();
        }
        

            /*
        public ActionResult Accounts()
        {



            List<Account> accountList = GetAccounts();
             GetAccounts().Wait();

            ViewBag.Title = "Accounts Page";
            ViewBag.Accounts = accountList;

            return View();
        }
        

        private async Task<List<Account>> GetAccounts()
        {
            //HttpClient httpClient = new HttpClient();
            //HttpRequestMessage request;

           // request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44380/api/accounts");

            // Send the request to the server
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44380/api/accounts");

            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;

            List<Account> accountList = JsonConvert.DeserializeObject<List<Account>>(json);
            //List<Account> accountList = JsonConvert.DeserializeObject<IEnumerable<Account>>(json);


            return accountList;
        }

    */

    }
}
