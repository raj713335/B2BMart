using B2BMart.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class AddressDTO
    {
        public int? AddressId { get; set; } = null;
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Address1 { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string ZipCode { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public AddressType AddressType { get; set; }
    }
}
