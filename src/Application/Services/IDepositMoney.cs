using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IDepositMoney
{
    public Task<ResultType> ExecuteAsyncDeposite(long accountId, int amount);
}