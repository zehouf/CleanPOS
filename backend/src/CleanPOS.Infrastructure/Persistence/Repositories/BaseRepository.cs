// src/CleanPOS.Infrastructure/Persistence/Repositories/BaseRepository.cs
namespace CleanPOS.Infrastructure.Persistence.Repositories;

using CleanPOS.Domain.Common;
using CleanPOS.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<T> : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(
        CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);
}