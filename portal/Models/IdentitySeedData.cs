using System;
using Microsoft.AspNetCore.Identity;

namespace portal.Models
{
    public class IdentitySeedData
    {
        private const string email = "boris.pouw@email.com";
        private const string password = "$Secret123456";

        private const string email2 = "stefi.nicoara@email.com";
        private const string password2 = "Makkelijk";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(email);
            if (user == null)
            {
                user = new IdentityUser("boris.pouw@email.com");
                await userManager.CreateAsync(user, password);
            }

            IdentityUser user2 = await userManager.FindByIdAsync(email2);
            if (user == null)
            {
                user = new IdentityUser("stefi.nicoara@email.com");
                await userManager.CreateAsync(user, password2);
            }
        }
    }

}

