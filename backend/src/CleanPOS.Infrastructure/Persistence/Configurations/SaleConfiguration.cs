// src/CleanPOS.Infrastructure/Persistence/Configurations/SaleConfiguration.cs
namespace CleanPOS.Infrastructure.Persistence.Configurations;

using CleanPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.CashierId)
               .IsRequired();

        builder.Property(s => s.SaleDate)
               .IsRequired();

        builder.Property(s => s.Status)
               .IsRequired()
               .HasConversion<int>();  // stocke l'enum comme int

        builder.Property(s => s.IsDeleted)
               .HasDefaultValue(false);

        // Total est calculé — ignoré par EF Core
        builder.Ignore(s => s.Total);

        // Filtre global
        builder.HasQueryFilter(s => !s.IsDeleted);

        // Navigation property — collection privée
        builder.Navigation(s => s.Lines)
               .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasQueryFilter(sl => !sl.IsDeleted);
    }
}