using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IAccountRepository
{
    Task<Account?> GetAccountByIdAsync(long accountId);

    Task UpdateAccountMoneyAsync(Account account, int amount);

    Task AddAccountAsync(Account account);
}