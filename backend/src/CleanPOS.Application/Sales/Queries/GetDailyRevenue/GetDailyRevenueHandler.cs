// src/CleanPOS.Application/Sales/Queries/GetDailyRevenue/GetDailyRevenueHandler.cs
namespace CleanPOS.Application.Sales.Queries.GetDailyRevenue;

using CleanPOS.Domain.Enums;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetDailyRevenueHandler
    : IRequestHandler<GetDailyRevenueQuery, decimal>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDailyRevenueHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<decimal> Handle(
        GetDailyRevenueQuery query,
        CancellationToken cancellationToken)
    {
        var sales = await _unitOfWork.Sales
            .GetByDateAsync(query.Date, cancellationToken);

        // Uniquement les ventes finalisées
        return sales
            .Where(s => s.Status == SaleStatus.Completed)
            .Sum(s => s.Total);
    }
}