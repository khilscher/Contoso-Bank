using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        [ReadOnly(true)]
        public string UserId { get; set; }

    }
}