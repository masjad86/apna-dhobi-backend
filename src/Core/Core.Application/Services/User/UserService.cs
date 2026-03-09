using ApnaDhobi.Core.Application.Interfaces.Services.User;
using ApnaDhobi.Core.Application.Interfaces;

namespace Core.Application.Services.User;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<IEnumerable<User>> GetUsersAsync()
    {
        await userRepository.
    }
}
