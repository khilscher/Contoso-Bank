using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ContosoBankApp.Models
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
