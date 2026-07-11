using System.ComponentModel.DataAnnotations;

namespace ProductManagementWebAPI.Models
{
    public class UpdateDto
    {
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
