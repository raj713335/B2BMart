using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class DeliveryMethodDTO
    {
        public string ShortName { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
