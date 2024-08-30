using B2BMart.Models.DTOs;
using B2BMart.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace B2BMart.Models
{
    public class AppUser
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 8 characters")]
        public string Password { get; set; } = null!;
        [Required]
        public UserType UserType { get; set; }
        //public AddressDTO? Address { get; set; }

    }
}
