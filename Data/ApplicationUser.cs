using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementWebAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
