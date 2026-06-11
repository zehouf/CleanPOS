// src/CleanPOS.Application/Categories/Queries/GetCategoryById/GetCategoryByIdQuery.cs
namespace CleanPOS.Application.Categories.Queries.GetCategoryById;

using CleanPOS.Application.Categories.DTOs;
using MediatR;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;