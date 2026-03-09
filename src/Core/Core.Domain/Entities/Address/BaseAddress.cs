namespace ApnaDhobi.Core.Domain.Entities;
public class BaseAddress {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Line1 { get; set; } = string.Empty;
    public string Line2 { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string FullAddress => $"{Street}, {City}, {State}, {PostalCode}, {Country}";
    public override string ToString()
    {
        return FullAddress;
    }
}