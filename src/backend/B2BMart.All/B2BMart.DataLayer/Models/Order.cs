using B2BMart.Models.Enums;
using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class Order : IAudit
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public bool IsDeleted { get; set; }
        public double Amount { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsPaid { get; set; }
        public int? UserId { get; set; }
        public int? DeliveryId { get; set; }
        public int? AddressId { get; set; }
        public string PaymentIntentId { get; set; } = null!;
        public string Status { get; set; } = OrderStatus.Pending.ToString()!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Address? Address { get; set; }
        public virtual DeliveryMethod? Delivery { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
