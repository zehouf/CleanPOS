// src/CleanPOS.Application/Categories/Queries/GetAllCategories/GetAllCategoriesQuery.cs
namespace CleanPOS.Application.Categories.Queries.GetAllCategories;

using CleanPOS.Application.Categories.DTOs;
using MediatR;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;