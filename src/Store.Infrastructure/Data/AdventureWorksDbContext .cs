using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Entities.Identity;
using Store.Infrastructure.Data.Configurations;

namespace Store.Infrastructure.Data
{
    public class AdventureWorksDbContext: DbContext
    {
        public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options) : base(options)
        {
        }

        public DbSet<Product>   Products     { get; set; }
        public DbSet<Category>  Categories   { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());

            // Alternative if you have many configurations:
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
