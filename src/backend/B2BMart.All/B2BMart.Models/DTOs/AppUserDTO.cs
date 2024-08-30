using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.DTOs
{
    public class AppUserDTO
    {
        public int UserId { get; set; }
        public string EmailId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Token { get; set; }
    }
}
