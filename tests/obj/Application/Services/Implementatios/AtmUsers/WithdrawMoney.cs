using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;

public class WithdrawMoney : IWithdrawMoney
{
    private readonly IAccountRepository _accountRepository;

    public WithdrawMoney(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ResultType> ExecuteAsyncWithdraw(long accountId, int amount)
    {
        Domain.Models.Account? account = await _accountRepository.GetAccountByIdAsync(accountId).ConfigureAwait(false);
        if (account == null)
            return new ResultType.Failure.WithdrawMoneyFailure.AccountNotFound();

        if (!account.WithdrawMoney(amount))
            return new ResultType.Failure.WithdrawMoneyFailure.InsufficientFunds();

        await _accountRepository.UpdateAccountMoneyAsync(account, -amount).ConfigureAwait(false);
        return new ResultType.Success();
    }
}