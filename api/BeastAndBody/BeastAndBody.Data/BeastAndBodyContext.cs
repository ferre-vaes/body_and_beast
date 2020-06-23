using System;
using BeastAndBody.Data.Models;
using BeastAndBody.Data.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeastAndBody.Data
{
    public class BeastAndBodyContext: IdentityDbContext<ApplicationUser, Role, int>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public BeastAndBodyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ApplicationUser>()
                .Property(u => u.Type)
                .HasConversion(
                    v => v.ToString(), 
                    v => (UserType)Enum.Parse(typeof(UserType), v));

            base.OnModelCreating(builder);
        }
    }
}
