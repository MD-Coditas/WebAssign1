using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAssign1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z]+\.[a-zA-Z]+$", ErrorMessage = "Email must be in the format 'example@domain.com' using only letters and digits.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }
    }
   
}
