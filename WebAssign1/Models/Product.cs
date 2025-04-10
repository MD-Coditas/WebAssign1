using System.ComponentModel.DataAnnotations;

namespace WebAssign1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 50)]
        public int Quantity { get; set; }
    }
}
