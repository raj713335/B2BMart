﻿using System;
using System.Collections.Generic;

namespace B2BMart.DataLayer.Models
{
    public partial class Role 
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public int Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
