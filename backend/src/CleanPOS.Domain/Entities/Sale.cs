// src/CleanPOS.Domain/Entities/Sale.cs
namespace CleanPOS.Domain.Entities;

using CleanPOS.Domain.Common;
using CleanPOS.Domain.Enums;

public class Sale : BaseEntity
{
    public string CashierId { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }
    public SaleStatus Status { get; private set; }

    private readonly List<SaleLine> _lines = [];
    public IReadOnlyCollection<SaleLine> Lines => _lines.AsReadOnly();
    public decimal Total => _lines.Sum(l => l.Subtotal);

    public ApplicationUser? Cashier { get; private set; }

    private Sale() { }

    public static Sale Create(string cashierId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cashierId);
        return new Sale
        {
            CashierId = cashierId,
            SaleDate = DateTime.UtcNow,
            Status = SaleStatus.Draft
        };
    }

    public void AddLine(SaleLine line)
    {
        ArgumentNullException.ThrowIfNull(line);

        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException(
                "Cannot add lines to a completed sale.");

        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException(
                "Cannot add lines to a cancelled sale.");

        _lines.Add(line);
        SetUpdatedAt();
    }

    public void Complete()
    {
        if (Status != SaleStatus.Draft)
            throw new InvalidOperationException(
                "Only draft sales can be completed.");

        if (!_lines.Any())
            throw new InvalidOperationException(
                "Cannot complete a sale with no lines.");

        Status = SaleStatus.Completed;
        SetUpdatedAt();
    }

    public void Cancel()
    {
        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException(
                "Cannot cancel a completed sale.");

        Status = SaleStatus.Cancelled;
        SetUpdatedAt();
    }
}