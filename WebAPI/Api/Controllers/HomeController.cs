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


        public async Task<ActionResult> Accounts()
        {
            string apiUrl = "http://contosobankapi.azurewebsites.net/api/accounts";
            List<Account> accountList = new List<Account>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    accountList = JsonConvert.DeserializeObject<List<Account>>(data);
                    ViewBag.Title = "Accounts Page";
                    ViewBag.Accounts = accountList;
                }


            }
            return View(accountList);

        }

    }
}
