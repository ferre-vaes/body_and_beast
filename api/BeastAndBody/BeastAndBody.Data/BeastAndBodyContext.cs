using System;
using BeastAndBody.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeastAndBody.Data
{
    public class BeastAndBodyContext: DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public BeastAndBodyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Activity>()
                .HasMany(a => a.Clients)
                .WithOne(a => a.Activity);

            base.OnModelCreating(builder);
        }
    }
}
