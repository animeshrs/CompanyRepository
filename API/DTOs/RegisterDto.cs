using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must contain at least 1 numeric, 1 lower, 1 upper case letter and should have length between 4 to 8 characters ")]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
