namespace ApnaDhobi.Core.Domain.Entities;
public class VendorAddress : BaseAddress
{
    public Guid VendorId { get; set; }    
    public Address PermanantAddress { get; set; } = new Address();
    public Address CurrentAddress { get; set; } = new Address();
}