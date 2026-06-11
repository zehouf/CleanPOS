// src/CleanPOS.Infrastructure/UnitOfWork.cs
namespace CleanPOS.Infrastructure;

using CleanPOS.Domain.Interfaces;
using CleanPOS.Domain.Interfaces.Repositories;
using CleanPOS.Infrastructure.Persistence;
using CleanPOS.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private ICategoryRepository? _categories;
    private IProductRepository? _products;
    private ISaleRepository? _sales;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ICategoryRepository Categories
        => _categories ??= new CategoryRepository(_context);

    public IProductRepository Products
        => _products ??= new ProductRepository(_context);

    public ISaleRepository Sales
        => _sales ??= new SaleRepository(_context);

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
        => _context.Dispose();
}