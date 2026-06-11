namespace CleanPOS.Domain.Entities;

using CleanPOS.Domain.Common;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }

    private Product() { }

    public static Product Create(string name, decimal price, int stock, Guid categoryId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (stock < 0) throw new ArgumentException("Stock cannot be negative.");
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId required.");
        return new Product
        {
            Name = name.Trim(),
            Price = price,
            Stock = stock,
            CategoryId = categoryId
        };
    }

    public void Update(string name, decimal price, int stock, Guid categoryId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (stock < 0) throw new ArgumentException("Stock cannot be negative.");
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId required.");
        Name = name.Trim(); Price = price; Stock = stock;
        CategoryId = categoryId; SetUpdatedAt();
    }

    public void DeductStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");
        if (quantity > Stock)
            throw new InvalidOperationException(
                $"Insufficient stock. Available: {Stock}, Requested: {quantity}");
        Stock -= quantity;
        SetUpdatedAt();
    }
    public void Delete()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Product is already deleted.");
        SoftDelete();
    }

    public new void Restore()
    {
        if (!IsDeleted)
            throw new InvalidOperationException("Product is not deleted.");
        base.Restore();
    }
}
