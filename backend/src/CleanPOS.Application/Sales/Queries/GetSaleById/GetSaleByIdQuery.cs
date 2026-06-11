// src/CleanPOS.Application/Sales/Queries/GetSaleById/GetSaleByIdQuery.cs
namespace CleanPOS.Application.Sales.Queries.GetSaleById;

using CleanPOS.Application.Sales.DTOs;
using MediatR;

public record GetSaleByIdQuery(Guid Id) : IRequest<SaleDto>;