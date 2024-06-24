using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10313014_P2.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductID { get; set; }
        [Required, MaxLength(40)]
        public string? ProductName { get; set; }
        [Required, MaxLength(40)]
        public string? ArtistName { get; set; }
        public string? ProductImage { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }

    }
}
