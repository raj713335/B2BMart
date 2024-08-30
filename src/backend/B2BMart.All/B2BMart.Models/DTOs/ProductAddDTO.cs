using B2BMart.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class ProductAddDTO
    {
        //[Required]
        //public int ProductId { get; set; }
        //public int IsDeleted { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; } = null!;
        [Required]
        [MaxLength(255)]
        public string ProductCode { get; set; } = null!;
        [Required]
        [MaxLength(3000)]
        public string Description { get; set; } = null!;
        [Required]
        [MaxLength(600)]
        public string PictureUrl { get; set; } = null!;
        [Required]
        public int ProductType { get; set; }
        [Required]
        public int ProductBrand { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Sellerid { get; set; }
        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; } = null!;
        //[Required]
        //[MaxLength(100)]
        //public string UpdatedBy { get; set; } = null!;
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
