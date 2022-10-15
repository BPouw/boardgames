using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure
{
    public class SecurityContext : IdentityDbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options): base(options)
        {
        }
    }
}
