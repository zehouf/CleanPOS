// src/CleanPOS.Infrastructure/Persistence/Configurations/SaleLineConfiguration.cs
namespace CleanPOS.Infrastructure.Persistence.Configurations;

using CleanPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleLineConfiguration : IEntityTypeConfiguration<SaleLine>
{
    public void Configure(EntityTypeBuilder<SaleLine> builder)
    {
        builder.HasKey(sl => sl.Id);

        builder.Property(sl => sl.Quantity)
               .IsRequired();

        builder.Property(sl => sl.UnitPrice)
               .HasPrecision(18, 2)
               .IsRequired();

        // Subtotal est calculé — ignoré par EF Core
        builder.Ignore(sl => sl.Subtotal);

        // Relation SaleLine → Sale
        builder.HasOne(sl => sl.Sale)
               .WithMany(s => s.Lines)
               .HasForeignKey(sl => sl.SaleId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relation SaleLine → Product
        builder.HasOne(sl => sl.Product)
               .WithMany()
               .HasForeignKey(sl => sl.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}