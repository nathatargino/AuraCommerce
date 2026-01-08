using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuraCommerce.Models;

namespace AuraCommerce.Data
{
    public class AuraCommerceContext : DbContext
    {
        public AuraCommerceContext(DbContextOptions<AuraCommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesRecord>()
                .HasOne(sr => sr.Seller)
                .WithMany(s => s.Sales)
                .HasForeignKey(sr => sr.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

           
             modelBuilder.Entity<Seller>()
                .HasOne(s => s.Department)
               .WithMany(d => d.Sellers)
               .HasForeignKey(s => s.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}