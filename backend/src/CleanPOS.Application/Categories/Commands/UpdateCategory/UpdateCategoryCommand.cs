// src/CleanPOS.Application/Categories/Commands/UpdateCategory/UpdateCategoryCommand.cs
namespace CleanPOS.Application.Categories.Commands.UpdateCategory;

using MediatR;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description
) : IRequest;