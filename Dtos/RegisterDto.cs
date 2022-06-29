using System.ComponentModel.DataAnnotations;

namespace PropertiesListings.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength =8)]
        public string Password { get; set; }

        [Required]
        [StringLength (50, MinimumLength = 8)]
        public string  ConfirmPassword { get; set; }
    }
}
