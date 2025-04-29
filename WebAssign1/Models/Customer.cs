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
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z]+\.[a-zA-Z]+$", ErrorMessage = "Email must be in the format 'example@domain.com' using only letters and digits.")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z0-9\s,/-]+$", ErrorMessage = "Invalid Address")]
        public string Address { get; set; }
    }
}
