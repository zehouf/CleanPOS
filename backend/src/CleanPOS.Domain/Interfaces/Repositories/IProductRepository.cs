namespace CleanPOS.Domain.Interfaces.Repositories;

using CleanPOS.Domain.Entities;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId,
                               CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name,
                               CancellationToken cancellationToken = default);
}
