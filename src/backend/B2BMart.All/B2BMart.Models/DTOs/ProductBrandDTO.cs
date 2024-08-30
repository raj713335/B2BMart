using System.ComponentModel.DataAnnotations;

namespace B2BMart.Models.DTOs
{
    public class ProductBrandDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
    }
}
