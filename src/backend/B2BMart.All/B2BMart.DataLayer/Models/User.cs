using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class User : IAudit
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            OrderItems = new HashSet<OrderItem>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string EmailId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
