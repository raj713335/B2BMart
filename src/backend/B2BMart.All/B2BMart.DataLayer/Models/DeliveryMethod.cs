using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class DeliveryMethod : IAudit
    {
        public DeliveryMethod()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryId { get; set; }
        public string ShortName { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
