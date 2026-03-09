namespace ApnaDhobi.Core.Application.Interfaces.Services.User;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
}
