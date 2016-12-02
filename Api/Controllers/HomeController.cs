using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Models;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Accounts()
        {
            AccountsController accounts = new AccountsController();
            List<Account> accountList = accounts.Get();
            ViewBag.Title = "Accounts Page";
            ViewBag.Accounts = accountList;

            return View();
        }
    }
}
