using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class ProductBrand : IAudit
    {
        public ProductBrand()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string ProductBrandName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
