using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickServe.Models;

namespace QuickServe.Data
{
    public class QuickServeContext : DbContext
    {
        public QuickServeContext (DbContextOptions<QuickServeContext> options)
            : base(options)
        {
        }

        public DbSet<QuickServe.Models.Product> Product { get; set; } = default!;

        public DbSet<QuickServe.Models.Users> Users { get; set; } = default!;

        public DbSet<QuickServe.Models.Orders> Orders { get; set; } = default!;

        public DbSet<QuickServe.Models.Comments> Comments { get; set; } = default!;

        public DbSet<QuickServe.Models.Cart> Cart { get; set; } = default!;
    }
}
