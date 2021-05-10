﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.UserName
            };

           await _userManager.CreateAsync(managerUser, "password");

            var adminClaim = new Claim("Role", "Admin");

            _userManager.AddClaimAsync(managerUser, adminClaim).GetAwaiter().GetResult();

            return true;
        }
    }
}
