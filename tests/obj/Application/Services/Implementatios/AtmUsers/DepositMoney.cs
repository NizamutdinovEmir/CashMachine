using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;

public class DepositMoney : IDepositMoney
{
    private readonly IAccountRepository _accountRepository;

    public DepositMoney(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ResultType> ExecuteAsyncDeposite(long accountId, int amount)
    {
        Domain.Models.Account? account = await _accountRepository.GetAccountByIdAsync(accountId).ConfigureAwait(false);
        if (account == null)
            return new ResultType.Failure.DepositMoneyFailure.AccountNotFound();

        if (!account.DepositMoney(amount))
            return new ResultType.Failure.DepositMoneyFailure.NegativeAmount();

        await _accountRepository.UpdateAccountMoneyAsync(account, amount).ConfigureAwait(false);
        return new ResultType.Success();
    }
}