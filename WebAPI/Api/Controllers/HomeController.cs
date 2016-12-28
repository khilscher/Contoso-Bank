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
using System.Security.Claims;

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

    }
}
