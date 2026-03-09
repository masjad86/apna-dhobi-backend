namespace ApnaDhobi.Core.Domain.Entities;
public class UserPermission: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.NewGuid();
}