using System.ComponentModel.DataAnnotations;

namespace ProductManagementWebAPI.Models
{
    public class SignUpModel
    {
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}