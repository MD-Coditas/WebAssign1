using System.ComponentModel.DataAnnotations;

namespace WebAssign1.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name must contain only letters and spaces.")]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(40)]
        public string Address { get; set; }
    }
}
