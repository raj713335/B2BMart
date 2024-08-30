using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class OrderItem : IAudit
    {
        public OrderItem()
        {
        }

        public OrderItem(int productid, double price, int quantity, int userid, int orderedId)
        {
            Productid = productid;
            Price = price;
            Quantity = quantity;
            UserId = userid;
            OrderId = orderedId;
        }

        public OrderItem(int productid, double price, int quantity, int userid, int orderedId, string statusDescription)
        {
            Productid = productid;
            Price = price;
            Quantity = quantity;
            UserId = userid;
            OrderId = orderedId;
            StatusDescription = statusDescription;
        }

        public int OrderItemId { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserId { get; set; }
        public int? Productid { get; set; }
        public int? OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
