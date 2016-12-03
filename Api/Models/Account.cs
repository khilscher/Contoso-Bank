using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Account
    {
        //Properties
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public double AccountBalance { get; set; }

    }
}