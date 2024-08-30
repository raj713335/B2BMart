using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class Product : IAudit
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public decimal Price { get; set; }
        public int Sellerid { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ProductBrand ProductBrand { get; set; } = null!;
        public virtual ProductType ProductType { get; set; } = null!;
        public virtual User Seller { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
