using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category mapping
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("ProductCategory", "Production");
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).HasColumnName("ProductCategoryID");
                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(e => e.SubCategories)
                      .WithOne(s => s.Category)
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // SubCategory mapping
            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("ProductSubcategory", "Production");
                entity.HasKey(e => e.SubCategoryId);
                entity.Property(e => e.SubCategoryId).HasColumnName("ProductSubcategoryID");
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.CategoryId).HasColumnName("ProductCategoryID");

                entity.HasMany(e => e.Products)
                      .WithOne(p => p.SubCategory)
                      .HasForeignKey(p => p.SubCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Product mapping
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Production");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ProductNumber).IsRequired();
                entity.Property(e => e.ListPrice);
                entity.Property(e => e.SubCategoryId).HasColumnName("ProductSubcategoryID");
            });
        }

    }
}
