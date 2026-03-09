using ApnaDhobi.Core.Domain.Enums;

namespace ApnaDhobi.Core.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public CustomerType Type { get; set; } = CustomerType.Regular;
    public StatusType Status { get; set; } = StatusType.Active;
    public IEnumerable<CustomerAddress> PermanantAddress { get; set; } = [];
    public IEnumerable<CustomerAddress> CurrentAddress { get; set; } = [new CustomerAddress()];    
}