// src/CleanPOS.Infrastructure/Persistence/Configurations/ProductConfiguration.cs
namespace CleanPOS.Infrastructure.Persistence.Configurations;

using CleanPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Price)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(p => p.Stock)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .HasDefaultValue(false);

        // Filtre global
        builder.HasQueryFilter(p => !p.IsDeleted);

        // Relation Product → Category
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}