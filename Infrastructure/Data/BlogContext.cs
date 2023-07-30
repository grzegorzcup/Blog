using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BlogContext : IdentityDbContext
    {
        public BlogContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public override int SaveChanges()
        {
            var entires = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entire in entires)
            {
                ((AuditableEntity)entire.Entity).LastModified = DateTime.UtcNow;

                if (entire.State == EntityState.Added)
                {
                    ((AuditableEntity)entire.Entity).Created = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Role>().HasData(
                    new Role()
                    {
                        Id = 1,
                        Name = "Admin",
                    },
                    new Role()
                    {
                        Id = 2,
                        Name = "User",
                    },
                    new Role()
                    {
                        Id = 3,
                        Name = "Moderator",
                    }
                );
        }
    }
}
