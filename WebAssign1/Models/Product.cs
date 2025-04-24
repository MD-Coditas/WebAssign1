using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAssign1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Name Should be less than 30 characters")]
        public string? Name { get; set; }
        [Required]
        [Range(1, 10000,ErrorMessage ="Price should be between 1 and 10000")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage ="Quantity should be between 1 and 100")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
    }
}
