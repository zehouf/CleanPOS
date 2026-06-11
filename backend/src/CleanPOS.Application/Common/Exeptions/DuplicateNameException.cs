// src/CleanPOS.Application/Common/Exceptions/DuplicateNameException.cs
namespace CleanPOS.Application.Common.Exceptions;

public class DuplicateNameException : Exception
{
    public DuplicateNameException(string entityName, string name)
        : base($"\"{entityName}\" with name \"{name}\" already exists.")
    { }
}