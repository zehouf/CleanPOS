// src/CleanPOS.Infrastructure/Persistence/Repositories/SaleRepository.cs
namespace CleanPOS.Infrastructure.Persistence.Repositories;

using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class SaleRepository
    : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(AppDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<Sale>> GetByDateAsync(
        DateTime date,
        CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(s => s.SaleDate.Date == date.Date)
            .Include(s => s.Lines)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Sale>> GetByCashierAsync(
        string cashierId,
        CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(s => s.CashierId == cashierId)
            .Include(s => s.Lines)
            .ToListAsync(cancellationToken);
}