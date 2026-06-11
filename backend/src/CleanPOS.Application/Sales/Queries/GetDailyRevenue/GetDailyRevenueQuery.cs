// src/CleanPOS.Application/Sales/Queries/GetDailyRevenue/GetDailyRevenueQuery.cs
namespace CleanPOS.Application.Sales.Queries.GetDailyRevenue;

using MediatR;

public record GetDailyRevenueQuery(DateTime Date) : IRequest<decimal>;