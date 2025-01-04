using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;

public class CreateAccount : ICreateAccount
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public CreateAccount(IAccountRepository accountRepository, IUserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public async Task<ResultType> ExecuteAsyncCreateAccount(long id, long userid, string name, int pinCode)
    {
        if (_accountRepository.GetAccountByIdAsync(id) != null)
            return new ResultType.Failure.CreateAccountFailure.AccountAlreadyExists();

        if (_userRepository.GetUserByIdAsync(userid) == null)
            return new ResultType.Failure.CreateAccountFailure.UserNotFound();

        var account = new Account(id, name, 0, pinCode, userid);
        await _accountRepository.AddAccountAsync(account).ConfigureAwait(false);
        return new ResultType.Success();
    }
}