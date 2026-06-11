// src/CleanPOS.Infrastructure/Persistence/Configurations/CategoryConfiguration.cs
namespace CleanPOS.Infrastructure.Persistence.Configurations;

using CleanPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .HasMaxLength(500);

        builder.Property(c => c.IsDeleted)
               .HasDefaultValue(false);

        // Filtre global — ignore automatiquement les supprimés
        builder.HasQueryFilter(c => !c.IsDeleted);

        // Navigation property — collection privée
        builder.Navigation(c => c.Products)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}