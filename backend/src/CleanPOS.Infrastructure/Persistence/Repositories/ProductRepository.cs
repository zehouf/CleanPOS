// src/CleanPOS.Infrastructure/Persistence/Repositories/ProductRepository.cs
namespace CleanPOS.Infrastructure.Persistence.Repositories;

using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductRepository
    : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(
            p => p.Name.ToLower() == name.ToLower(),
            cancellationToken);
}