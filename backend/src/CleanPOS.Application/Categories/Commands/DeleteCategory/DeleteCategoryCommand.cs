// src/CleanPOS.Application/Categories/Commands/DeleteCategory/DeleteCategoryCommand.cs
namespace CleanPOS.Application.Categories.Commands.DeleteCategory;

using MediatR;

public record DeleteCategoryCommand(Guid Id) : IRequest;