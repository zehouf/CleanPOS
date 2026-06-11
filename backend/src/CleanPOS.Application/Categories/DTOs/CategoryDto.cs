// src/CleanPOS.Application/Categories/DTOs/CategoryDto.cs
namespace CleanPOS.Application.Categories.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Description,
    int ProductCount,
    DateTime CreatedAt
);