using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
        public int? DeliveryMethodId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string PaymentIntentId { get; set; } = null!;
        //public decimal ShippingPrice { get; set; }
    }
}
