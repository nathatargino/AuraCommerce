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
        public AuraCommerceContext (DbContextOptions<AuraCommerceContext> options)
            : base(options)
        {
        }

        public DbSet<AuraCommerce.Models.Department> Department { get; set; } = default!;
    }
}
