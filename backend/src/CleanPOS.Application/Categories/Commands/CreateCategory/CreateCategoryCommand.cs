// src/CleanPOS.Application/Categories/Commands/CreateCategory/CreateCategoryCommand.cs
namespace CleanPOS.Application.Categories.Commands.CreateCategory;

using MediatR;

public record CreateCategoryCommand(
    string Name,
    string? Description
) : IRequest<Guid>;