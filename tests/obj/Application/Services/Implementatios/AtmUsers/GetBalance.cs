using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;

public class GetBalance : IGetBalance
{
    private readonly IDisplay _display;
    private readonly IAccountRepository _accountRepository;

    public GetBalance(IAccountRepository accountRepository, IDisplay display)
    {
        _accountRepository = accountRepository;
        _display = display;
    }

    public async Task<ResultType> ExecuteAsyncBalance(long accountId)
    {
        Domain.Models.Account? account = await _accountRepository.GetAccountByIdAsync(accountId).ConfigureAwait(false);
        if (account == null)
            return new ResultType.Failure.GetBalanceFailure.AccountNotFound();

        _display.Display($"Current balance: {account.Balance}");
        return new ResultType.Success();
    }
}