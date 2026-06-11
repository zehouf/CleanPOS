namespace CleanPOS.Domain.Entities;

using CleanPOS.Domain.Common;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }  // Constructeur privé pour EF Core

    public static Category Create(string name, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return new Category
        {
            Name = name.Trim(),
            Description = description?.Trim()
        };
    }

    public void Update(string name, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
        Description = description?.Trim();
        SetUpdatedAt();
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Category is already deleted.");
        SoftDelete();
    }

    public new void Restore()
    {
        if (!IsDeleted)
            throw new InvalidOperationException("Category is not deleted.");
        IsDeleted = false;
        DeletedAt = null;
        SetUpdatedAt();
    }
}
