using Azure;
using System.ComponentModel.DataAnnotations;


namespace ABCRetail_ST10313014_CLDPOEFinal.Models
{
    public class Cart
    {
        [Key]
        public int Cart_ID { get; set; }   

        public double? Product_Price { get; set; }

        public string? Address { get; set; }
        public string? PaymentType { get; set; }

        [Required(ErrorMessage = "Please select a Customer.")]
        public int Customer_ID { get; set; }

        [Required(ErrorMessage ="Please select a Product")]

        public int Product_ID { get; set; }


        
    }
}
