using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.User;

public class LoginUser : ILoginUser
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDisplay _display;

    public LoginUser(IAccountRepository accountRepository, IDisplay display)
    {
        _accountRepository = accountRepository;
        _display = display;
    }

    public async Task<ResultType> AuthorizationUser(long id, int pinCode)
    {
        Account? account = await _accountRepository.GetAccountByIdAsync(id).ConfigureAwait(false);
        if (account == null)
        {
            _display.Display("Account not found.");
            return new ResultType.Failure.LoginFailure.AccountNotFound();
        }

        if (account.PinCode != pinCode)
        {
            _display.Display("Invalid PIN code.");
            return new ResultType.Failure.LoginFailure.InvalidPinCode();
        }

        return new ResultType.Success();
    }
}