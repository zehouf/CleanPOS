// src/CleanPOS.Infrastructure/Persistence/AppDbContext.cs
namespace CleanPOS.Infrastructure.Persistence;

using CleanPOS.Domain.Entities;
using CleanPOS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<AppIdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleLine> SaleLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Applique automatiquement toutes les configurations
        // dans le dossier Configurations/
        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}