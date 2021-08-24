using Data.Constants;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<OrderPersonnel> OrderPersonnel { get; set; }
        public DbSet<AccountEntity> AccountEntities { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountMovement> AccountMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<OrderType>()
                .HasData(
                    new OrderType { Id = OrderTypeId.Dovme.GetHashCode(), Name = "Dövme", CrtDate = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Local) },
                    new OrderType { Id = OrderTypeId.MakePiercing.GetHashCode(), Name = "Make Piercing", CrtDate = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
