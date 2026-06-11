// src/CleanPOS.Infrastructure/Persistence/Repositories/CategoryRepository.cs
namespace CleanPOS.Infrastructure.Persistence.Repositories;

using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository
    : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context)
        : base(context)
    { }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(
            c => c.Name.ToLower() == name.ToLower(),
            cancellationToken);
}