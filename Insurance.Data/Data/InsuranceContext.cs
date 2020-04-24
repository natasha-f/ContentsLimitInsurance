using Insurance.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Data.Data
{
    public class InsuranceContext : DbContext
    {
    //    public static string ConnectionString { get; set; }

        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
        {
        }
        
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
