using System;
using Microsoft.EntityFrameworkCore;
using Asp_Dot_Net_Web_Api.Models;

namespace Asp_Dot_Net_Web_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User and Order
            modelBuilder.Entity<User>().HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId);
            // User and Review
            modelBuilder.Entity<User>().HasMany(u => u.Reviews).WithOne(o => o.User).HasForeignKey(o => o.UserId);
            // Book and Review
            modelBuilder.Entity<Book>().HasMany(u => u.Reviews).WithOne(o => o.Book).HasForeignKey(o => o.BookId);

            // Category and SubCategory
            modelBuilder.Entity<Category>().HasMany(u => u.SubCategories).WithOne(o => o.Category).HasForeignKey(o => o.CategoryId);

            // SubCategory and Book
            modelBuilder.Entity<SubCategory>().HasMany(u => u.Books).WithOne(o => o.SubCategory).HasForeignKey(o => o.SubCategoryId);

            modelBuilder.Entity<BookOrder>().HasKey(bo => new { bo.BookId, bo.OrderId });

            modelBuilder.Entity<BookOrder>()
                .HasOne(bo => bo.Book)
                .WithMany(b => b.BookOrders)
                .HasForeignKey(bo => bo.BookId);

            modelBuilder.Entity<BookOrder>()
                .HasOne(bo => bo.Order)
                .WithMany(o => o.BookOrders)
                .HasForeignKey(bo => bo.OrderId);

            //
        }
    }
}

