using Microsoft.EntityFrameworkCore;
using PermissionSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSystem.Persistence
{
    public class PermissionDbContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionType>()
                .HasMany(pt => pt.Permissions)
                .WithOne(p => p.PermissionType)
                .HasForeignKey(p => p.PermissionTypeId);

            modelBuilder.Entity<PermissionType>().HasData(
               new PermissionType { Id = 1, Description = "Sick" },
               new PermissionType { Id = 2, Description = "Vacations" },
               new PermissionType { Id = 3, Description = "Personal things" }
           );
        }
    }
}
