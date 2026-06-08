namespace CleanPOS.Domain.Entities;

// Pas de dépendance à ASP.NET Identity ici
// Identity est un détail d'Infrastructure
public class ApplicationUser
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    private readonly List<Sale> _sales = [];
    public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
}
