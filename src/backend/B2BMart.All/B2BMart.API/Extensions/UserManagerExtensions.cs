﻿using B2BMart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace B2BMart.API.Extensions
{
    public static class UserManagerExtensions
    {
        //public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        //{
        //    var email = user.FindFirstValue(ClaimTypes.Email);

        //    return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        //}

        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.SingleOrDefaultAsync(x => x.EmailId == email);
        }
    }
}
