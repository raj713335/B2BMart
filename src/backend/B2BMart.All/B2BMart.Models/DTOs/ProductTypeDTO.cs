using System.ComponentModel.DataAnnotations;

namespace B2BMart.Models.DTOs
{
    public class ProductTypeDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
