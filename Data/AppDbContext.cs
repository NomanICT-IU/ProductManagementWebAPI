using Microsoft.EntityFrameworkCore;

namespace ProductManagementWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }


        //Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "Dell Inspiron 15, Intel Core i5, 16GB RAM, 512GB SSD",
                    Price = 75000.00M,
                    Quantity = 10,
                    CreatedDate = new DateTime(2025, 1, 1)
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Description = "Samsung Galaxy A55, 8GB RAM, 256GB Storage",
                    Price = 45000.00M,
                    Quantity = 20,
                    CreatedDate = new DateTime(2025, 1, 2)
                },
                new Product
                {
                    Id = 3,
                    Name = "Wireless Mouse",
                    Description = "Logitech M331 Silent Plus Wireless Mouse",
                    Price = 1500.00M,
                    Quantity = 50,
                    CreatedDate = new DateTime(2025, 1, 3)
                },
                new Product
                {
                    Id = 4,
                    Name = "Mechanical Keyboard",
                    Description = "Redragon K552 RGB Mechanical Gaming Keyboard",
                    Price = 3500.00M,
                    Quantity = 30,
                    CreatedDate = new DateTime(2025, 1, 4)
                },
                new Product
                {
                    Id = 5,
                    Name = "Monitor",
                    Description = "LG 24-inch Full HD IPS Monitor",
                    Price = 18000.00M,
                    Quantity = 15,
                    CreatedDate = new DateTime(2025, 1, 5)
                }
            );


        }

        //set Created and Updated time
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            SetDateTime();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetDateTime()
        {
            var entries = ChangeTracker.Entries<Product>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.Now;
                }
            }
        }
    }
}
