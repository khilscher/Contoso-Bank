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
using System.Security.Claims;

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
        /// <summary>
        /// Gets a list of accounts.
        /// </summary>
        [ResponseType(typeof(List<Account>))]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(List<Account>))]
        public IHttpActionResult Get()
        {

            List<Account> foundAccounts = new List<Account>();

            //Obtain caller's UserId from claim
            string owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            //Find all accounts belonging to user
            foundAccounts = accountList.FindAll(account => account.UserId == owner);

            return Ok(foundAccounts);
        }

        // GET api/accounts/1001
        /// <summary>
        /// Gets a specific account by account number.
        /// </summary>
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

            //Obtain caller's UserId from claim
            string owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            //Find specified account belonging to user
            foundAccount = accountList.Find(account => (account.AccountNumber == accountNumber) && (account.UserId == owner));

            if (foundAccount == null)
            {
                ErrorResponse error = new ErrorResponse { Message = "Account not found" };
                return Content(HttpStatusCode.NotFound, error);
            }

            return Ok(foundAccount);
        }

        // POST api/accounts
        /// <summary>
        /// Creates a new account.
        /// </summary>
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
            //Obtain caller's UserId from claim
            string owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            Account foundAccount = new Account();

            //Find specified account belonging to user
            foundAccount = accountList.Find(account => (account.AccountNumber == accountInfo.AccountNumber) && (account.UserId == owner));

            //If no duplicate account already exists, create new account.
            if (foundAccount == null)
            {
                //Append UserId
                accountInfo.UserId = owner;

                //Add to static field. This should actually be persisted to a DB.
                accountList.Add(accountInfo);

                return CreatedAtRoute("DefaultApi", new { id = accountInfo.AccountNumber}, accountInfo);
            }

            ErrorResponse error = new ErrorResponse { Message = "Duplicate account already exists" };

            return Content(HttpStatusCode.Conflict, error);
        }

        // PUT api/accounts/1001
        /// <summary>
        /// Updates an existing account.
        /// </summary>
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
            //Obtain caller's UserId from claim
            string owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            Account acctToUpdate = new Account();

            //Find specified account belonging to user
            acctToUpdate = accountList.Find(account => (account.AccountNumber == accountNumber) && (account.UserId == owner));

            //If account is found, update it.
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
        /// <summary>
        /// Deletes an account.
        /// </summary>
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
            //Obtain caller's UserId from claim
            string owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            Account acctToDelete = new Account();

            //Find specified account belonging to user
            acctToDelete = accountList.Find(account => (account.AccountNumber == accountNumber) && (account.UserId == owner));

            //If found, delete it
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
