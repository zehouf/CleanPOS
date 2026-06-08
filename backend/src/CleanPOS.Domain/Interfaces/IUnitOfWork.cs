namespace CleanPOS.Domain.Interfaces;

using CleanPOS.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    ISaleRepository Sales { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
