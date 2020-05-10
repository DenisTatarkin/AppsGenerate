

using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Controllers;
using WebApp.Models.Entities;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<UserProject> UserProject { get; set; }
    }
}
 
 