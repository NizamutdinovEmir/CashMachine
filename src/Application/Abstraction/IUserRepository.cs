using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IUserRepository
{
    Task<IUser?> GetUserByIdAsync(long id);

    Task CreateUserAsync(AtmUser user);

    Task<IUser?> GetUserByNameAsync(string name);

    Task<IEnumerable<IUser>> GetAllUsersAsync();
}