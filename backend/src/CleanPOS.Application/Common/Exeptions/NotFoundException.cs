// src/CleanPOS.Application/Common/Exceptions/NotFoundException.cs
namespace CleanPOS.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    { }

    public NotFoundException(string message)
        : base(message)
    { }
}