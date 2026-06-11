// src/CleanPOS.Application/Sales/Queries/GetSaleById/GetSaleByIdHandler.cs
namespace CleanPOS.Application.Sales.Queries.GetSaleById;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Application.Sales.DTOs;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetSaleByIdHandler
    : IRequestHandler<GetSaleByIdQuery, SaleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto> Handle(
        GetSaleByIdQuery query,
        CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales
            .GetByIdAsync(query.Id, cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale", query.Id);

        return new SaleDto(
            sale.Id,
            sale.CashierId,
            sale.SaleDate,
            sale.Status,
            sale.Total,
            sale.Lines.Select(l => new SaleLineDto(
                l.ProductId,
                l.Product?.Name ?? string.Empty,
                l.Quantity,
                l.UnitPrice,
                l.Subtotal
            )),
            sale.CreatedAt
        );
    }
}