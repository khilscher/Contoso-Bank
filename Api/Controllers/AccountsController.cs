using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;
using Swashbuckle.Swagger.Annotations;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AccountsController : ApiController
    {

        static List<Account> accountList = new List<Account>();
        static bool isInitialized;

        public AccountsController()
        {
            //Constructor
            if (!isInitialized)
            {
                Account initalAccount = new Account();
                initalAccount.AccountNumber = 1001;
                initalAccount.AccountType = "Savings";
                initalAccount.AccountBalance = 49999.99;

                accountList.Add(initalAccount);

                isInitialized = true;
            }
        }

        /*
        // GET api/accounts
        public List<Account> Get()
        {
            return accountList;
        }
        */

        // GET api/accounts
        [ResponseType(typeof(List<Account>))]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(List<Account>))]
        public IHttpActionResult Get()
        {
            return Ok(accountList);
        }

        // GET api/accounts/1001
        [ResponseType(typeof(Account))]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(Account))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Not Found",
            Type = typeof(ErrorResponse))]
        [Route("api/accounts/{accountNumber}")]
        public IHttpActionResult GetAccount([FromUri] int accountNumber)
        {
            Account foundAccount = new Account();

            foundAccount = accountList.Find(account => account.AccountNumber == accountNumber);

            if (foundAccount == null)
            {
                ErrorResponse error = new ErrorResponse { Message = "Account not found" };
                return Content(HttpStatusCode.NotFound, error);
            }

            return Ok(foundAccount);
        }

        // POST api/accounts
        [ResponseType(typeof(Account))]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "Created",
            Type = typeof(Account))]
        [SwaggerResponse(HttpStatusCode.Conflict,
            Description = "Conflict",
            Type = typeof(ErrorResponse))]
        public IHttpActionResult Post([FromBody]Account accountInfo)
        {

            Account foundAccount = new Account();
            foundAccount = accountList.Find(account => account.AccountNumber == accountInfo.AccountNumber);

            if (foundAccount == null)
            {
                accountList.Add(accountInfo);
                return CreatedAtRoute("DefaultApi", new { id = accountInfo.AccountNumber}, accountInfo);
            }

            ErrorResponse error = new ErrorResponse { Message = "Duplicate account already exists" };
            return Content(HttpStatusCode.Conflict, error);
        }

        // PUT api/accounts/1001
        [ResponseType(typeof(void))]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "Updated",
            Type = typeof(Account))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Account not found",
            Type = typeof(ErrorResponse))]
        [Route("api/accounts/{accountNumber}")]
        public IHttpActionResult Put([FromUri] int accountNumber, [FromBody]Account updatedAcctInfo)
        {

            Account acctToUpdate = new Account();
            acctToUpdate = accountList.Find(account => account.AccountNumber == accountNumber);
            if (acctToUpdate != null)
            {
                acctToUpdate.AccountNumber = updatedAcctInfo.AccountNumber;
                acctToUpdate.AccountType = updatedAcctInfo.AccountType;
                acctToUpdate.AccountBalance = updatedAcctInfo.AccountBalance;

                return Ok(acctToUpdate);
            }

            ErrorResponse error = new ErrorResponse { Message = "Account not found" };
            return Content(HttpStatusCode.NotFound, error);

        }


        // DELETE api/accounts/1001
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(bool))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Account not found",
            Type = typeof(bool))]
        [ResponseType(typeof(Account))]
        [Route("api/accounts/{accountNumber}")]
        public IHttpActionResult Delete([FromUri] int accountNumber)
        {
            Account acctToDelete = new Account();
            acctToDelete = accountList.Find(account => account.AccountNumber == accountNumber);
            if (acctToDelete != null)
            {
                accountList.Remove(acctToDelete);
                return Ok(acctToDelete);
            }

            ErrorResponse error = new ErrorResponse { Message = "Account not found" };
            return Content(HttpStatusCode.NotFound, error);
        }
        
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
    }
}
