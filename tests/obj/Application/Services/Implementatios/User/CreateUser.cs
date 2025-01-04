using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.User;

public class CreateUser : ICreateUser
{
    private readonly IUserRepository _userRepository;

    public CreateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultType> ExecuteAsync(long id, UserRole role, string userName, string password)
    {
        if (_userRepository.GetUserByIdAsync(id) != null)
            return new ResultType.Failure.CreateUserFailure.UserAlreadyExists();

        var user = new AtmUser(id, role, userName, password);
        await _userRepository.CreateUserAsync(user).ConfigureAwait(false);
        return new ResultType.Success();
    }
}