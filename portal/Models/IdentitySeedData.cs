using System;
using Microsoft.AspNetCore.Identity;

namespace portal.Models
{
    public class IdentitySeedData
    {
        private const string email = "boris.pouw@email.com";
        private const string password = "$Secret123456";

        private const string email2 = "ntstefi@email.com";

        private const string email3 = "piet@email.com";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(email);
            if (user == null)
            {
                user = new IdentityUser("boris.pouw@email.com");
                await userManager.CreateAsync(user, password);
            }

            IdentityUser user2 = await userManager.FindByIdAsync(email2);
            if (user2 == null)
            {
                user2 = new IdentityUser("ntstefi@email.com");
                await userManager.CreateAsync(user2, password);
            }

            IdentityUser user3 = await userManager.FindByIdAsync(email3);
            if (user3 == null)
            {
                user3 = new IdentityUser("piet@email.com");
                await userManager.CreateAsync(user3, password);
            }
        }
    }

}

