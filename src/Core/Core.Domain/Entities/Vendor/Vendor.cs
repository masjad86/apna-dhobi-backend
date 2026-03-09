using Core.Domain.Entities;

namespace ApnaDhobi.Core.Domain.Entities;

public class Vendor: TrackableEntity
{
    public Guid VendorId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public IEnumerable<VendorAddress> Addresses { get; set; } = [];
    
}