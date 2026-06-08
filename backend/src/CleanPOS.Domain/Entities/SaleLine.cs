namespace CleanPOS.Domain.Entities;

using CleanPOS.Domain.Common;

public class SaleLine : BaseEntity
{
    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    // Calculé — pas stocké en base de données
    public decimal Subtotal => Quantity * UnitPrice;

    public Sale? Sale { get; private set; }
    public Product? Product { get; private set; }

    private SaleLine() { }

    public static SaleLine Create(Guid saleId, Guid productId,
                                  int quantity, decimal unitPrice)
    {
        if (saleId == Guid.Empty) throw new ArgumentException("SaleId required.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId required.");
        if (quantity <= 0) throw new ArgumentException("Quantity > 0.");
        if (unitPrice < 0) throw new ArgumentException("UnitPrice >= 0.");
        return new SaleLine
        {
            SaleId = saleId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
}
