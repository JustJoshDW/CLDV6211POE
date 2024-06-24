using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10313014_P2.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
        public List<Product> Product { get; set; }
    }
}
