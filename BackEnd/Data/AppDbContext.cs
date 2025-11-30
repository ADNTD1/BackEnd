using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Ecomerce_Back_End.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<GraphicsCard> Graficas { get; set; }

        public DbSet<Motherboard> Motherboards { get; set; }

        public DbSet<PowerSupply> PowerSupplies { get; set; }

        public DbSet<Ram> Ram { get; set; }

        public DbSet<CpuCooler> CpuCoolers { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<Processors> Processors { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Computers> Computers { get; set; } 

        public DbSet<Laptop> laptops { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<GraphicsCard>().ToTable("Graficas");
            modelBuilder.Entity<Motherboard>().ToTable("Motherboards");
            modelBuilder.Entity<PowerSupply>().ToTable("PowerSupplies");
            modelBuilder.Entity<Ram>().ToTable("Rams");
            modelBuilder.Entity<CpuCooler>().ToTable("CpuCoolers");
            modelBuilder.Entity<Storage>().ToTable("Storages");
            modelBuilder.Entity<Case>().ToTable("Cases");
            modelBuilder.Entity<Processors>().ToTable("Processors");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<User>().ToTable("Users");

        }
    }
}
