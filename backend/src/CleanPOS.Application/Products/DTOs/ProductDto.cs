// src/CleanPOS.Application/Products/DTOs/ProductDto.cs
namespace CleanPOS.Application.Products.DTOs;

public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    Guid CategoryId,
    string CategoryName,
    DateTime CreatedAt
);