using Microsoft.EntityFrameworkCore;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shoper.Persistance.Context
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-ERDEM\\SQLEXPRESS; database=Shoper; Integrated Security=True; TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>()
        //        .HasMany(c=>c.Products)
        //        .WithOne(p=>p.Category)
        //        .HasForeignKey(p => p.CategoryId);

        //    // Category - Product (1'e Çok)
        //    modelBuilder.Entity<Products>()
        //        .HasOne(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryId);

        //    modelBuilder.Entity<Customer>()
        //        .HasMany(c => c.Orders)
        //        .WithOne(o => o.Customer)
        //        .HasForeignKey(o => o.CustomerId);

        //    // Customer - Order (1'e Çok)
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.Customer)
        //        .WithMany(c => c.Orders)
        //        .HasForeignKey(o => o.CustomerId);

        //    modelBuilder.Entity<Order>()
        //        .HasMany(o => o.OrderItems)
        //        .WithOne(oi => oi.Order)
        //        .HasForeignKey(oi => oi.OrderId);



        //    // Order - OrderItem (1'e Çok)
        //    modelBuilder.Entity<OrderItem>()
        //        .HasOne(oi => oi.Order)
        //        .WithMany(o => o.OrderItems)
        //        .HasForeignKey(oi => oi.OrderId);

        //}

    }
}
