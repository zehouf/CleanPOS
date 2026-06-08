namespace CleanPOS.Domain.Interfaces.Repositories;

using CleanPOS.Domain.Entities;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}

