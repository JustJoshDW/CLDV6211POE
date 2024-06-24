using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10313014_P2.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        public bool IsDeleted { get; set;} = false;
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
