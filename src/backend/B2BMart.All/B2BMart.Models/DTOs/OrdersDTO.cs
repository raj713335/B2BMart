using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class OrdersDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int IsDeleted { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int IsCancelled { get; set; }
        [Required]
        public int IsPaid { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int DeliveryId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public string Status { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
        [Required]
        public string UpdatedBy { get; set; } = null!;
        [Required]
        public string BasketId { get; set; } = null!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public string CreatedAt { get; set; } = null!;
        [Required]
        public string UpdatedAt { get; set; } = null!;
    }
}
