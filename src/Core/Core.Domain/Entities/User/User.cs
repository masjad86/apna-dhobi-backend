using ApnaDhobi.Core.Domain.Entities;
namespace ApnaDhobiCore.Domain.Entities;

public class User: TrackableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserGroup Group { get; set; } = new();
    public UserRole Role { get; set; } = new();
    public IEnumerable<UserPermission> Vendors { get; set; } = [];
}