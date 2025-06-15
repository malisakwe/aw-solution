using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Production");

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId)
                .HasColumnName("ProductID"); // Maps to AdventureWorks' ProductID column

            // Properties
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ProductNumber)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(p => p.ListPrice)
                .HasColumnType("money");

            // Foreign Key - Critical Mapping
            builder.Property(p => p.SubCategoryId)
                .HasColumnName("ProductSubcategoryID"); // Maps to AdventureWorks column

            // Relationship
            builder.HasOne(p => p.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SubCategoryId)
                .HasConstraintName("FK_Product_ProductSubcategory_ProductSubcategoryID")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
