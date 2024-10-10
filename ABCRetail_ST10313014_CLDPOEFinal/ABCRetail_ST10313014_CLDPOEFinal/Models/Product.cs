using Azure;
using System.ComponentModel.DataAnnotations;

namespace ABCRetail_ST10313014_CLDPOEFinal.Models
{
    public class Product
    {
        [Key]
        public int Product_ID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl {  get; set; }
        public double? Product_Price { get; set; }

    }
}
