using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


//Based on https://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client

namespace ApiTestConsoleApp
{
    public class Account
    {
        //Properties
        public int AccountNumber { get; set; }
        
        public string AccountType { get; set; }
        
        public double AccountBalance { get; set; }

    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowAccount(Account account)
        {
            Console.WriteLine($"Number: {account.AccountNumber}\tType: {account.AccountType}\tBalance: {account.AccountBalance}");
        }

        static async Task<Uri> CreateAccountAsync(Account account)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/accounts", account);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Account> GetAccountAsync(string path)
        {
            Account account = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                account = await response.Content.ReadAsAsync<Account>();
            }
            return account;
        }

        static async Task<Account> UpdateAccountAsync(Account account)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/accounts/{account.AccountNumber}", account);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            account = await response.Content.ReadAsAsync<Account>();
            return account;
        }

        static async Task<HttpStatusCode> DeleteAccountAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/accounts/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://contosobankapi.azurewebsites.net/"); 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Account account = new Account { AccountNumber = 1002, AccountType = "Investment", AccountBalance = 50000.99 };

                var url = await CreateAccountAsync(account);
                Console.WriteLine($"Created at {url}");

                // Get the product
                account = await GetAccountAsync(url.PathAndQuery);
                ShowAccount(account);

                // Update the product
                Console.WriteLine("Updating account balance...");
                account.AccountBalance = 80;
                await UpdateAccountAsync(account);

                // Get the updated product
                account = await GetAccountAsync(url.PathAndQuery);
                ShowAccount(account);

                // Delete the product
                var statusCode = await DeleteAccountAsync(account.AccountNumber);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }
}
