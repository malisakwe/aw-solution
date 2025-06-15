using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Data.Configurations
{
    
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("ProductCategory", "Production");

            // Primary Key
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryId)
                .HasColumnName("ProductCategoryID"); // AdventureWorks column name

            // Properties
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
    
}
