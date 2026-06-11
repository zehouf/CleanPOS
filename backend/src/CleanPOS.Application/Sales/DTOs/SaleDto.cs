// src/CleanPOS.Application/Sales/DTOs/SaleDto.cs
namespace CleanPOS.Application.Sales.DTOs;

using CleanPOS.Domain.Enums;

public record SaleLineDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);

public record SaleDto(
    Guid Id,
    string CashierId,
    DateTime SaleDate,
    SaleStatus Status,
    decimal Total,
    IEnumerable<SaleLineDto> Lines,
    DateTime CreatedAt
);