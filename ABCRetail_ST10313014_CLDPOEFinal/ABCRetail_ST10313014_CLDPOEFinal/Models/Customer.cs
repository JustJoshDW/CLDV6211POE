using Azure;
using System.ComponentModel.DataAnnotations;

namespace ABCRetail_ST10313014_CLDPOEFinal.Models
{
    public class Customer
    {        
        [Key]
        public int CustID { get; set; }
       
        public string? CustName { get; set; }
        
        public string? CustEmail { get; set; }
        
        public string? Password { get; set; }

        public DateTime? CustBirthDate { get; set; }

    }

    
}
