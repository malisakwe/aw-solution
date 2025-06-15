using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Data.Configurations
{

    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("ProductSubcategory", "Production");


            // Primary Key
            builder.HasKey(s => s.SubCategoryId);
            builder.Property(s => s.SubCategoryId)
                .HasColumnName("ProductSubcategoryID"); // AdventureWorks column name

            // Properties
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(400);

            // Foreign Key
            builder.Property(s => s.CategoryId)
                .HasColumnName("ProductCategoryID"); // AdventureWorks column name

            // Relationship
            builder.HasOne(s => s.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId)
                .HasConstraintName("FK_ProductSubcategory_ProductCategory_ProductCategoryID")
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
