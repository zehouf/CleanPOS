namespace CleanPOS.Domain.Entities;

using CleanPOS.Domain.Common;

public class Sale : BaseEntity
{
    public string CashierId { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }

    private readonly List<SaleLine> _lines = [];
    public IReadOnlyCollection<SaleLine> Lines => _lines.AsReadOnly();

    // Total calculé — Single Source of Truth
    public decimal Total => _lines.Sum(l => l.Subtotal);

    public ApplicationUser? Cashier { get; private set; }

    private Sale() { }

    public static Sale Create(string cashierId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cashierId);
        return new Sale { CashierId = cashierId, SaleDate = DateTime.UtcNow };
    }

    public void AddLine(SaleLine line)
    {
        ArgumentNullException.ThrowIfNull(line);
        _lines.Add(line);
    }
}
