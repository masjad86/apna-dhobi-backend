namespace ApnaDhobi.Core.Domain.Entities;

public class UserRole : BaseEntity  
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}