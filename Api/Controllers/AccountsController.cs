using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class AccountsController : ApiController
    {

        static List<Account> accountList = new List<Account>();

        public AccountsController()
        {
            //Constructor
        }

        // GET api/accounts
        //public IEnumerable<Account> Get()
        public List<Account> Get()
        {
            return accountList;
        }

        // GET api/accounts/5
        public string Get(int id)
        {
            //TODO
            return "value";
        }

        // POST api/accounts
        public void Post([FromBody]Account accountInfo)
        {
            accountList.Add(accountInfo);
        }

        // PUT api/accounts/5
        public void Put(int id, [FromBody]string value)
        {
            //TODO
        }

        // DELETE api/accounts/5
        public void Delete(int id)
        {
            //TODO
        }
    }
}
