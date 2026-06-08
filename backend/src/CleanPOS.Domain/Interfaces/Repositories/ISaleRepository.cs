namespace CleanPOS.Domain.Interfaces.Repositories;

using CleanPOS.Domain.Entities;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<IEnumerable<Sale>> GetByDateAsync(DateTime date,
                            CancellationToken cancellationToken = default);
    Task<IEnumerable<Sale>> GetByCashierAsync(string cashierId,
                            CancellationToken cancellationToken = default);
}
