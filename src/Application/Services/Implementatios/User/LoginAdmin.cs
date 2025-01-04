using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.User;

public class LoginAdmin : ILoginAdmin
{
    private readonly IUserRepository _userRepository;
    private readonly IDisplay _display;

    public LoginAdmin(IUserRepository userRepository, IDisplay display)
    {
        _userRepository = userRepository;
        _display = display;
    }

    public async Task<ResultType> AuthorizationAdmin(string name, string password)
    {
        IUser? admin = await _userRepository.GetUserByNameAsync(name).ConfigureAwait(false);
        if (admin == null)
        {
            _display.Display("User not found.");
            return new ResultType.Failure.LoginFailure.UserNotFound();
        }

        if (admin.Role != UserRole.Admin)
        {
            _display.Display("Access denied. You are not an admin.");
            return new ResultType.Failure.LoginFailure.NotAdmin();
        }

        if (admin.Password != password)
        {
            _display.Display("Password incorrect");
            return new ResultType.Failure.LoginFailure.WrongPassword();
        }

        _display.Display("Admin logged in successfully.");
        return new ResultType.Success();
    }
}